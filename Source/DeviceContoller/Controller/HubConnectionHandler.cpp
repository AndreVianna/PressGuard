#include "HubConnectionHandler.h"
#include "LogHandler.h"

#include <string>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/server.hpp>

HubConnectionHandler::HubConnectionHandler(
    const ushort deviceId,
    LogHandler* logger)
    : DeviceId(deviceId)
    , IsConnected(false)
    , Logger(logger) {
    Server.init_asio();
    Server.set_open_handler([this](const ConnectionHandle& connection) {
        Connection = connection;
        IsConnected = true;
    });
    Server.set_close_handler([this](const ConnectionHandle& connection) {
        IsConnected = false;
        Connection = connection;
    });
    Server.set_message_handler([this](auto&& ph1, auto&& ph2) {
        OnDataReceived(std::forward<decltype(ph2)>(ph2));
    });
}

void HubConnectionHandler::Start() {
    Server.listen(DeviceId);
    Server.start_accept();
    Server.run();
}

void HubConnectionHandler::RegisterAction(const std::string& key, ActionDelegate action) {
    Actions[key] = std::move(action);
}

void HubConnectionHandler::SendData(const std::string& data) {
    const auto outgoingMessage = std::to_string(DeviceId) + "," + data;
    Logger->LogInfo("Sending: " + outgoingMessage);
    if (!IsConnected) {
        Logger->LogWarning("Connection is not open. Message not sent.");
        return;
    }

    Server.send(Connection, outgoingMessage, websocketpp::frame::opcode::text);
    Logger->LogDebug("Sent: " + outgoingMessage);
}

void HubConnectionHandler::OnDataReceived(const IncomingData& data) {
    const std::string payload = data->get_payload();
    Logger->LogInfo("Received: " + payload);

    const auto  pos = payload.find(':');
    if (pos == std::string::npos) {
        Logger->LogWarning("Invalid payload. Message ignored.");
        return;
    }

    const auto key = payload.substr(0, pos);
    if (const auto action = Actions.find(key); action != Actions.end()) {
        Logger->LogDebug("Action '" + key + "' found.");
        action->second(payload.substr(pos + 1));
        Logger->LogDebug("Action '" + key + "' executed.");
        return;
    }

    Logger->LogWarning("Action '" + key + "' not found.");
}

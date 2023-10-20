#include "HubConnectionHandler.h"
#include "LogHandler.h"

#include <websocketpp/frame.hpp>

HubConnectionHandler::HubConnectionHandler(
    const ushort deviceId,
    LogHandler* logger)
    : DeviceId(deviceId)
    , IsConnected(false)
    , Logger(logger) {
    Logger->LogDebug("Initializing hub.");
    Server.init_asio();
    Server.set_open_handler([this](const ConnectionHandle& connection) {
        Logger->LogDebug("Open requested.");
        if (IsConnected) return;
        Logger->LogDebug("Opening requested.");
        Connection = connection;
        IsConnected = true;
        Logger->LogDebug("Opened.");
    });
    Server.set_close_handler([this](const ConnectionHandle&) {
        Logger->LogDebug("Close requested.");
        if (!IsConnected) return;
        Logger->LogDebug("Closing connection.");
        IsConnected = false;
        Logger->LogDebug("Closed..");
    });
    Server.set_message_handler([this](auto&&, auto&& ph2) {
        OnDataReceived(std::forward<decltype(ph2)>(ph2));
    });

    Server.set_validate_handler([this](const ConnectionHandle& connection) {
        Logger->LogDebug("HTTP validation requested.");
        return true; // return false to reject connection
                                });

    // Handler for ping
    Server.set_ping_handler([this](const ConnectionHandle& connection, std::string payload) {
        Logger->LogDebug("Ping received with payload: " + payload);
        return true; // return false to reject ping
                            });

    // Handler for pong
    Server.set_pong_handler([this](const ConnectionHandle& connection, std::string payload) {
        Logger->LogDebug("Pong received with payload: " + payload);
        return true; // return false to reject pong
    });

    // Handler for pong timeout
    Server.set_pong_timeout_handler([this](const ConnectionHandle& connection, std::string payload) {
        Logger->LogError("Pong timeout with payload: " + payload);
        return true; // return false to terminate connection
    });

    // Handler for failures
    Server.set_fail_handler([this](const ConnectionHandle& connection) {
        Logger->LogError("Connection failed.");
    });
    Logger->LogDebug("Hub initialized.");
}

void HubConnectionHandler::Start() {
    Logger->LogDebug("Starting hub.");
    Server.listen(DeviceId);
    Server.start_accept();
    //std::thread serverThread([&] {
    //    Logger->LogDebug("Hub running...");
    //    Server.run();
    //});
    //serverThread.detach();
    Logger->LogDebug("Hub started.");
}

void HubConnectionHandler::RegisterAction(const std::string& key, ActionDelegate action) {
    Actions[key] = std::move(action);
}

void HubConnectionHandler::SendData(const std::string& data) {
    if (Logger->Level == -1) std::cout << 'S' << std::flush;
    if (!IsConnected) {
        Logger->LogWarning("Connection is not open. Data not sent.");
        if (Logger->Level == -1) std::cout << '!' << std::flush;
        return;
    }

    const auto outgoingMessage = std::to_string(DeviceId) + "," + data;
    Logger->LogInfo("Sending: " + outgoingMessage);
    Server.send(Connection, outgoingMessage, websocketpp::frame::opcode::text);
}

void HubConnectionHandler::PollData() {
    Server.poll();
    if (Logger->Level == -1) std::cout << 'P' << std::flush;
}

void HubConnectionHandler::OnDataReceived(const IncomingData& data) {
    const std::string payload = data->get_payload();
    Logger->LogInfo("Received: " + payload);

    const auto  pos = payload.find(':');
    if (pos == std::string::npos) {
        Logger->LogDebug("Invalid payload. Message ignored.");
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

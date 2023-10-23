#include "WebSocketConnection.h"

#include <websocketpp/frame.hpp>

WebSocketConnection::WebSocketConnection(
    const ushort deviceId,
    LogHandler* logger)
    : DeviceId(deviceId)
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
    Server.set_message_handler([this](const ConnectionHandle& connection, const IncomingData& message) {
        Logger->LogDebug("Message received.");
        if (!IsConnected) return;
        Logger->LogDebug("Message received.");
        const auto messageText = message->get_payload();
        Logger->LogInfo("Received: " + messageText);
        if (messageText == "ping") {
            Logger->LogDebug("Sending pong.");
            Server.send(connection, "pong", websocketpp::frame::opcode::text);
        }
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

void WebSocketConnection::Start() {
    Logger->LogDebug("Starting hub...");
    Server.listen(DeviceId);
    Server.start_accept();
    //std::thread serverThread([&] {
    //    Logger->LogDebug("Hub running...");
    //    Server.run();
    //});
    //serverThread.detach();
    Logger->LogDebug("Hub started.");
}

void WebSocketConnection::Stop() {
    Logger->LogDebug("Stopping hub...");
    Server.stop_listening();
    Logger->LogDebug("Hub stopped.");
}

void WebSocketConnection::SendData(const std::string& data) {
    if (!IsConnected) {
        Logger->LogWarning("Connection is not open. Data not sent.");
        return;
    }

    const auto outgoingMessage = std::to_string(DeviceId) + "," + data;
    Logger->LogInfo("Sending: " + outgoingMessage);
    Server.send(Connection, outgoingMessage, websocketpp::frame::opcode::text);
}

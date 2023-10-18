#pragma once

#include "LogHandler.h"

#include <bits/stdint-uintn.h>
#include <websocketpp/common/connection_hdl.hpp>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/endpoint.hpp>
#include <websocketpp/roles/server_endpoint.hpp>


class HubConnectionHandler {
public:
    using ConnectionHandle = websocketpp::connection_hdl;
    using Socket = websocketpp::server<websocketpp::config::asio>;
    using IncomingData = Socket::message_ptr;
    using ActionDelegate = std::function<void(const std::string&)>;
    using Dictionary = std::unordered_map<std::string, ActionDelegate>;

    explicit HubConnectionHandler(ushort deviceId,
                                  LogHandler* logger);

    void Start();
    void SendData(const std::string& data);
    void RegisterAction(const std::string& key, ActionDelegate action);
private:
    uint16_t DeviceId; // Use the connection port as device Id.
    Socket Server;
    ConnectionHandle Connection;
    bool IsConnected;
    Dictionary Actions;
    LogHandler* Logger;

    void OnDataReceived(const IncomingData& data);
};

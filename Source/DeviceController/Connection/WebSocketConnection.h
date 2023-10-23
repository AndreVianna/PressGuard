#pragma once

#include <websocketpp/common/connection_hdl.hpp>
#include <websocketpp/config/asio_no_tls.hpp>
#include <websocketpp/endpoint.hpp>
#include <websocketpp/roles/server_endpoint.hpp>

#include "../Common/LogHandler.h"

using namespace std;
using namespace chrono;
using namespace websocketpp;
using namespace websocketpp::config;

using Socket = server<asio>;
using ConnectionHandle = connection_hdl;
using IncomingData = Socket::message_ptr;

class WebSocketConnection {
public:
    explicit WebSocketConnection(
        ushort deviceId,
        LogHandler* logger = new LogHandler());

    ushort DeviceId;
    bool IsConnected;

    void Start();
    void SendData(const string& data);
    void Stop();
private:
    Socket Server;
    ConnectionHandle Connection;
    LogHandler* Logger;
};

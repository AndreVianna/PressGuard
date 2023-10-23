#pragma once

#include "../DataAccess/FileDataStore.h"
#include "../Connection/WebSocketConnection.h"
#include "../SensorReader/SensorReader.h"

class DeviceHandler {
public:
    explicit DeviceHandler(
        WebSocketConnection* hub,
        SensorReader* reader = new SensorReader(),
        FileDataStore* store = new FileDataStore(),
        LogHandler* logger = new LogHandler());
    void ProcessSignals() const;
private:
    SensorReader* Reader;
    FileDataStore* Store;
    WebSocketConnection* Hub;
    LogHandler* Logger;
};
#include "DeviceHandler.h"

#include <pigpio.h>
#include <stdexcept>

DeviceHandler::DeviceHandler(
    WebSocketConnection* hub,
    SensorReader* reader,
    FileDataStore* store,
    LogHandler* logger)
    : Reader(reader)
    , Store(store)
    , Hub(hub)
    , Logger(logger) {
    Reader->Initialize();
}

void DeviceHandler::ProcessSignals() const
{
    auto dataLine = Reader->ReadSensors();
    Logger->LogInfo(dataLine);
    dataLine = Store->Save(dataLine);
    Hub->SendData(dataLine);
    Reader->Sleep();
}

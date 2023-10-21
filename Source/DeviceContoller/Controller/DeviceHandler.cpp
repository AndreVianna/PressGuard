#include "DeviceHandler.h"
#include "FileDataStore.h"
#include "HubConnectionHandler.h"
#include "LogHandler.h"
#include "Messages.h"

#include <pigpio.h>
#include <stdexcept>

DeviceHandler::DeviceHandler(
    FileDataStore* store,
    HubConnectionHandler* hub,
    LogHandler* logger,
    const int numberOfSensors,
    const int delayBetweenScansInMilliseconds)
    : NumberOfSensors(numberOfSensors)
    , DelayBetweenScansInMilliseconds(delayBetweenScansInMilliseconds)
    , Store(store)
    , Hub(hub)
    , Logger(logger) {
    Initialize();
}

DeviceHandler::~DeviceHandler() {
    gpioTerminate();
}

void DeviceHandler::ProcessSignals() const
{
    std::string dataLine;
    for (auto pin = 0; pin < NumberOfSensors; ++pin) {
        ReadSensor(pin, dataLine);
    }

    Logger->LogInfo(dataLine);
    dataLine = Store->Save(dataLine);
    Hub->SendData(dataLine);
    Hub->PollData();

    Sleep();
}

void DeviceHandler::Initialize() const {
    Logger->LogDebug("Initializing Handler.");
    if (const auto initResult = gpioInitialise(); initResult < 0) {
        const auto message = Messages::GetFormattedMessage("InitFail", std::to_string(initResult));
        Logger->LogError(message);
        throw std::runtime_error(message);
    }

    for (auto i = 0; i < NumberOfSensors; ++i) {
        gpioSetMode(i, PI_INPUT);
    }
    Logger->LogDebug("Handler initialized.");
}


void DeviceHandler::Sleep() const {
    gpioDelay(DelayBetweenScansInMilliseconds * 1000);
}

void DeviceHandler::ReadSensor(int pin, std::string& dataLine) const {
    if (const auto sensorValue = gpioRead(pin); sensorValue >= 0) {
        dataLine += (dataLine.length() == 0 ? "" : ",") + std::to_string(sensorValue);
        return;
    }

    Logger->LogWarning(Messages::GetFormattedMessage("SensorFail", std::to_string(pin)));
}

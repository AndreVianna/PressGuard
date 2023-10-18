#include "FileDataStore.h"
#include "DeviceHandler.h"
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
    Initialise();
}

DeviceHandler::~DeviceHandler() {
    gpioTerminate();
}

void DeviceHandler::ProcessSignals() const
{
    std::string dataLine;
    for (auto pin = 0; pin < NumberOfSensors; ++pin) {
        if (const auto sensorValue = gpioRead(pin); sensorValue >= 0) {
            dataLine += (dataLine.length() == 0 ? "" : ",") + std::to_string(sensorValue);
        }
        else {
            Logger->LogError(Messages::GetFormattedMessage("SensorFail", std::to_string(pin)));
        }
    }

    Logger->LogInfo(dataLine);
    dataLine = Store->Save(dataLine);
    Hub->SendData(dataLine);
    Sleep();
}

void DeviceHandler::Initialise() const {
    if (const auto initResult = gpioInitialise(); initResult < 0) {
        const auto message = Messages::GetFormattedMessage("InitFail", std::to_string(initResult));
        Logger->LogError(message);
        throw std::runtime_error(message);
    }

    for (auto i = 0; i < NumberOfSensors; ++i) {
        gpioSetMode(i, PI_INPUT);
    }
}


void DeviceHandler::Sleep() const {
    gpioDelay(DelayBetweenScansInMilliseconds * 1000);
}

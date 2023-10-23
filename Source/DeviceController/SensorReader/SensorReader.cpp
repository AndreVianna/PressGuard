#include "SensorReader.h"
#include "Messages.h"

#include <pigpio.h>
#include <stdexcept>

SensorReader::SensorReader(
    const int numberOfSensors,
    const int delayBetweenScansInMilliseconds,
    LogHandler* logger)
    : NumberOfSensors(numberOfSensors)
    , DelayBetweenScansInMilliseconds(delayBetweenScansInMilliseconds)
    , Logger(logger) {
    Initialize();
}

SensorReader::~SensorReader() {
    gpioTerminate();
}

void SensorReader::Initialize() const {
    Logger->LogDebug("Initializing Handler.");
    if (const auto initResult = gpioInitialise(); initResult < 0) {
        const auto message = Messages::GetFormattedMessage("InitFail", to_string(initResult));
        Logger->LogError(message);
        throw runtime_error(message);
    }

    for (auto i = 0; i < NumberOfSensors; ++i) {
        gpioSetMode(i, PI_INPUT);
    }
    Logger->LogDebug("Handler initialized.");
}


void SensorReader::Sleep() const {
    gpioDelay(DelayBetweenScansInMilliseconds * 1000);
}

[[nodiscard]] string SensorReader::ReadSensors() const {
    string dataLine;
    for (auto pin = 0; pin < NumberOfSensors; ++pin) {
        ReadSensor(pin, dataLine);
    }
    return dataLine;
}

void SensorReader::ReadSensor(const int pin, string& dataLine) const {
    if (const auto sensorValue = gpioRead(pin); sensorValue >= 0) {
        dataLine += (dataLine.length() == 0 ? "" : ",") + to_string(sensorValue);
        return;
    }

    Logger->LogWarning(Messages::GetFormattedMessage("SensorFail", to_string(pin)));
}

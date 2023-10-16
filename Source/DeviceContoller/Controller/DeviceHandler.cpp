#include "DeviceHandler.h"
#include "Messages.h"
#include <iostream>
#include <pigpio.h>
#include <stdexcept>

DeviceHandler::DeviceHandler(const int numberOfSensors, const int delayBetweenScansInMicroseconds)
    : NumberOfSensors(numberOfSensors)
    , DelayBetweenScansInMicroseconds(delayBetweenScansInMicroseconds) {
    Initialise();
}

DeviceHandler::~DeviceHandler() {
    gpioTerminate();
}

void DeviceHandler::ProcessSignals() const
{
    for (int pin = 0; pin < NumberOfSensors; ++pin) {
        if (const int sensorValue = gpioRead(pin); sensorValue >= 0) {
            std::cout << Messages::GetFormattedMessage("SensorRead", std::to_string(pin)) << sensorValue << '\n';
        }
        else {
            std::cerr << Messages::GetFormattedMessage("SensorFail", std::to_string(pin)) << '\n';
        }
    }

    Sleep();
}

void DeviceHandler::Initialise() const {
    if (const auto initResult = gpioInitialise(); initResult < 0) {
        throw std::runtime_error(Messages::GetFormattedMessage("InitFail", std::to_string(initResult)));
    }

    for (int i = 0; i < NumberOfSensors; ++i) {
        gpioSetMode(i, PI_INPUT);
    }
}


void DeviceHandler::Sleep() const {
    gpioDelay(DelayBetweenScansInMicroseconds);
}

void DeviceHandler::Terminate() {
    gpioTerminate();
}

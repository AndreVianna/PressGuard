#include "DeviceHandler.h"
#include <iostream>
#include <stdexcept>
#include <string>

int main(const int argc, char* argv[]) {
    const int numSensors = argc > 1 ? std::stoi(argv[1]) : DeviceHandler::DEFAULT_NUMBER_OF_SENSORS;
    const int sleepDelay = argc > 2 ? std::stoi(argv[2]) : DeviceHandler::DEFAULT_DELAY_BETWEEN_SCANS_IN_MICROSECONDS;

    try {
        const DeviceHandler deviceHandler(numSensors, sleepDelay);

        while (true) {
            deviceHandler.ProcessSignals();
        }
    }
    catch (const std::exception& e) {
        std::cerr << "An exception occurred: " << e.what() << std::endl;
    }

    return 0;
}

#include "MockDateTimeProvider.h"
#include "MockDeviceHandler.h"

#include <iostream>
#include <random>


std::mt19937 Randomizer(std::random_device);
std::uniform_int_distribution<> range(0, 50);

MockDeviceHandler::MockDeviceHandler() {
    DateTime = new MockDateTimeProvider("20210101", "12:34:56.123");
}

void MockDeviceHandler::ProcessSignals() const {
    const auto time = DateTime->GetFormattedTime();
    auto dataLine = "9876," + time;
    for (auto i = 0; i < 8; ++i) {
        dataLine += "," + std::to_string(range(Randomizer));
    }
    std::cout << dataLine << std::endl;
}

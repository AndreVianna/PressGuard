#include "../Domain/DeviceHandler.h"

#include <sys/types.h>

using namespace std;

int main(const int argc, char* argv[]) {
    const auto logLevel = argc > 1 ? stoi(argv[1]) : 2;
    const ushort deviceId = argc > 2 ? static_cast<ushort>(stoul(argv[2])) : 9002;
    const auto numSensors = argc > 3 ? stoi(argv[3]) : SensorReader::DEFAULT_NUMBER_OF_SENSORS;
    const auto sleepDelay = argc > 4 ? stoi(argv[4]) : SensorReader::DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS;

    DateTimeProvider dateTime{};
    LogHandler logger(logLevel, &dateTime);

    try {
        SensorReader reader(numSensors, sleepDelay, &logger);
        FileDataStore store(&dateTime);
        WebSocketConnection  hub(deviceId, &logger);
        const DeviceHandler deviceHandler(&hub, &reader, &store, &logger);

        hub.Start();

        while (true) {
            deviceHandler.ProcessSignals();
        }
    }
    catch (const std::exception& e) {
        logger.LogError("An exception occurred: " + string(e.what()) + "\n");
    }

    return 0;
}

#include "FileDataStore.h"
#include "DeviceHandler.h"
#include "HubConnectionHandler.h"
#include "LogHandler.h"

int main(const int argc, char* argv[]) {
    const auto logLevel = argc > 1 ? std::stoi(argv[1]) : 2;
    const ushort deviceId = argc > 2 ? static_cast<ushort>(std::stoul(argv[2])) : 9002;
    const auto numSensors = argc > 3 ? std::stoi(argv[3]) : DeviceHandler::DEFAULT_NUMBER_OF_SENSORS;
    const auto sleepDelay = argc > 4 ? std::stoi(argv[4]) : DeviceHandler::DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS;

    auto logger = LogHandler::CreateLogger(logLevel);

    try {
        auto store = FileDataStore::Create();
        HubConnectionHandler hub(deviceId, &logger);

        const DeviceHandler deviceHandler(&store, &hub, &logger, numSensors, sleepDelay);

        while (true) {
            deviceHandler.ProcessSignals();
        }
    }
    catch (const std::exception& e) {
        logger.LogError("An exception occurred: " + std::string(e.what()) + "\n");
    }

    return 0;
}

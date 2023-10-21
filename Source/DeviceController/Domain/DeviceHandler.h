#pragma once

#include "FileDataStore.h"
#include "HubConnectionHandler.h"
#include "LogHandler.h"

#include "IDeviceHandler.h"

class DeviceHandler : public IDeviceHandler {
public:
    static constexpr int DEFAULT_NUMBER_OF_SENSORS = 8;
    static constexpr int DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS = 500; // 0.5s

    explicit DeviceHandler(FileDataStore* store,
                           HubConnectionHandler* hub,
                           LogHandler* logger,
                           int numberOfSensors = DEFAULT_NUMBER_OF_SENSORS,
                           int delayBetweenScansInMilliseconds = DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS);
    void ProcessSignals() const override;
    ~DeviceHandler() override;
private:
    int NumberOfSensors;
    int DelayBetweenScansInMilliseconds;
    FileDataStore* Store;
    HubConnectionHandler* Hub;
    LogHandler* Logger;

    void Initialize() const;
    void ReadSensor(int pin, std::string& dataLine) const;
    void Sleep() const;
};

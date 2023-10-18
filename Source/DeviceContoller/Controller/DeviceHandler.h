#pragma once

#include "DataFileHandler.h"
#include "HubConnectionHandler.h"
#include "LogHandler.h"

class DeviceHandler {
public:
    static constexpr int DEFAULT_NUMBER_OF_SENSORS = 8;
    static constexpr int DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS = 500; // 0.5s

    explicit DeviceHandler(DataFileHandler* dataFile,
                           HubConnectionHandler* hub,
                           LogHandler* logger,
                           int numberOfSensors = DEFAULT_NUMBER_OF_SENSORS,
                           int delayBetweenScansInMilliseconds = DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS);
    ~DeviceHandler();
    void ProcessSignals() const;
private:
    int NumberOfSensors;
    int DelayBetweenScansInMilliseconds;
    DataFileHandler* DataFile;
    HubConnectionHandler* Hub;
    LogHandler* Logger;

    void Initialise() const;
    void Sleep() const;
};

#pragma once

class DeviceHandler {
public:
    static constexpr int DEFAULT_NUMBER_OF_SENSORS = 8;
    static constexpr int DEFAULT_DELAY_BETWEEN_SCANS_IN_MICROSECONDS = 500000; // 500ms

    explicit DeviceHandler(const int numberOfSensors = DEFAULT_NUMBER_OF_SENSORS,
                           const int delayBetweenScansInMicroseconds = DEFAULT_DELAY_BETWEEN_SCANS_IN_MICROSECONDS);
    ~DeviceHandler();
    void ProcessSignals() const;
private:
    int NumberOfSensors;
    int DelayBetweenScansInMicroseconds;

    void Initialise() const;
    void Sleep() const;
    static void Terminate();
};

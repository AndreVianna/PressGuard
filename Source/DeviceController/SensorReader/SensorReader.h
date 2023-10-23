#pragma once

#include <string>

#include "../Common/LogHandler.h"

using namespace std;

class SensorReader {
public:
    static constexpr int DEFAULT_NUMBER_OF_SENSORS = 8;
    static constexpr int DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS = 500; // 0.5s

    explicit SensorReader(int numberOfSensors = DEFAULT_NUMBER_OF_SENSORS, 
                          int delayBetweenScansInMilliseconds = DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS,
                          LogHandler * logger = new LogHandler());
    void Initialize() const;
    [[nodiscard]] string ReadSensors() const;
    void Sleep() const;

    ~SensorReader();
private:
    int NumberOfSensors;
    int DelayBetweenScansInMilliseconds;
    LogHandler* Logger;

    void ReadSensor(int pin, string& dataLine) const;
};

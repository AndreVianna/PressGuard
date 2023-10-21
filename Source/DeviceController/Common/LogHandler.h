#pragma once

#include "IDateTimeProvider.h"

#include <fstream>
#include <iosfwd>

class LogHandler {
public:
    explicit LogHandler(int level, IDateTimeProvider* dateTime);
    void LogDebug(const std::string& message);
    void LogInfo(const std::string& message);
    void LogWarning(const std::string& message);
    void LogError(const std::string& message);

    int Level;

    ~LogHandler();
protected:
    void Log(const std::string& level, const std::string& message);
private:
    void OpenOrCreateLogFile();
    void CloseLogFile();
    IDateTimeProvider* DateTime;
    std::ofstream File;
    std::string Name;
};

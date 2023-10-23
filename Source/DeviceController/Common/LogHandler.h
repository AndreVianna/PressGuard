#pragma once

#include "DateTimeProvider.h"
#include <fstream>

class LogHandler {
public:
    explicit LogHandler();
    explicit LogHandler(int level);
    explicit LogHandler(int level, DateTimeProvider* dateTime);
    LogHandler(const LogHandler& other);
    LogHandler& operator=(const LogHandler& other);
    ~LogHandler();

    [[nodiscard]] int GetLogLevel() const;
    void LogDebug(const string& message);
    void LogInfo(const string& message);
    void LogWarning(const string& message);
    void LogError(const string& message);
private:
    int Level;

    void Log(const string& level, const string& message);
    void OpenOrCreateLogFile();
    void CloseLogFile();

    DateTimeProvider* DateTime;
    ofstream File;
    string Name;
};

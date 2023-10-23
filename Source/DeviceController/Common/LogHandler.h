#pragma once

#include "DateTimeFormatter.h"
#include "SystemFile.h"

class LogHandler {
public:
    explicit LogHandler(const int level = 2, DateTimeFormatter* dateTime = new DateTimeFormatter(), File* file = new SystemFile());
    ~LogHandler();

    [[nodiscard]] int GetLogLevel() const;
    void LogDebug(const string& message);
    void LogInfo(const string& message);
    void LogWarning(const string& message);
    void LogError(const string& message);
private:
    int _level;

    void Log(const string& level, const string& message);
    void OpenOrCreateLogFile();
    void CloseLogFile() const;

    DateTimeFormatter* _dateTime;
    File* _file;
    string _name;
};

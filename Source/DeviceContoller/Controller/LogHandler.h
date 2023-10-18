#pragma once

#include <fstream>
#include <string>

class LogHandler {
public:
    LogHandler();
    static LogHandler CreateLogger(int level);
    void LogDebug(const std::string& message);
    void LogInfo(const std::string& message);
    void LogWarning(const std::string& message);
    void LogError(const std::string& message);
protected:
    void Log(const std::string& level, const std::string& message);
private:
    void CreateLogFile();
    void CloseLogFile();
    std::ofstream File;
    std::string Name;
    int Level;
};

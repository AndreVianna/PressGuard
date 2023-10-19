#pragma once

#include <fstream>
#include <iosfwd>

class LogHandler {
public:
    LogHandler(int level);
    void LogDebug(const std::string& message);
    void LogInfo(const std::string& message);
    void LogWarning(const std::string& message);
    void LogError(const std::string& message);
    ~LogHandler();
protected:
    void Log(const std::string& level, const std::string& message);
private:
    void OpenOrCreateLogFile();
    void CloseLogFile();
    std::ofstream File;
    std::string Name;
    int Level;
};

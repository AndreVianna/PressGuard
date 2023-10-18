// LogHandler.cpp

#include "DateTimeProvider.h"
#include "LogHandler.h"
#include <iostream>
#include <stdexcept>

LogHandler::LogHandler() = default;

LogHandler LogHandler::CreateLogger(const int level) {
    LogHandler handler;
    handler.Level = level;
    handler.CreateLogFile();
    return handler;
}

void LogHandler::LogDebug(const std::string& message) {
    if (Level > 0) return;
    Log("DEBUG", message);
}

void LogHandler::LogInfo(const std::string& message) {
    if (Level > 1) return;
    Log("INFO", message);
}

void LogHandler::LogWarning(const std::string& message) {
    if (Level > 2) return;
    Log("WARN", message);
}

void LogHandler::LogError(const std::string& message) {
    if (Level > 3) return;
    Log("ERROR", message);
}

void LogHandler::Log(const std::string& level, const std::string& message) {
    if (const auto currentDay = DateTimeProvider::GetFormattedDate(); Name != currentDay) {
        CloseLogFile();
        Name = currentDay;
        CreateLogFile();
    }
    if (!File.is_open()) {
        throw std::runtime_error("Log file is not open.");
    }

    const auto output = DateTimeProvider::GetFormattedTime() + ": [" + level + "] " + message;
    File << output << "\n" << std::flush;
    std::cerr << output << std::endl;
}

void LogHandler::CloseLogFile() {
    if (File.is_open()) {
        File.close();
    }
}

void LogHandler::CreateLogFile() {
    File.open(Name + ".log", std::ios::app);
    if (!File.is_open()) {
        throw std::runtime_error("Failed to open or create log file.");
    }

}

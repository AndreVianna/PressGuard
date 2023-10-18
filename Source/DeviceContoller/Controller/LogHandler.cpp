// LogHandler.cpp

#include "DateTimeProvider.h"
#include "LogHandler.h"

#include <iosfwd>
#include <iostream>
#include <ostream>
#include <stdexcept>

LogHandler::LogHandler() = default;

LogHandler LogHandler::CreateLogger(const int level) {
    LogHandler handler;
    handler.Level = level;
    handler.OpenOrCreateLogFile();
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
    OpenOrCreateLogFile();
    const auto output = DateTimeProvider::GetFormattedTime() + ": [" + level + "] " + message;
    File << output << "\n" << std::flush;
    std::cerr << output << std::endl;
}

void LogHandler::CloseLogFile() {
    if (File.is_open()) {
        File.close();
    }
}

void LogHandler::OpenOrCreateLogFile() {
    if (const auto currentDay = DateTimeProvider::GetFormattedDate(); Name != currentDay) {
        CloseLogFile();
        Name = currentDay;
    }
    File.open(Name + ".log", std::ios::app);
    if (!File.is_open()) {
        throw std::runtime_error("Failed to open or create log file.");
    }
}

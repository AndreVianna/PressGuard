// LogHandler.cpp

#include "DateTimeProvider.h"
#include "LogHandler.h"

#include <iosfwd>
#include <iostream>
#include <ostream>
#include <stdexcept>

LogHandler::LogHandler(const int level) {
    Level = level;
    OpenOrCreateLogFile();
}

LogHandler::~LogHandler() {
    CloseLogFile();
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
    File.flush();
    std::cerr << output << std::endl;
}

void LogHandler::CloseLogFile() {
    if (!File.is_open()) return;
    File.close();
}

void LogHandler::OpenOrCreateLogFile() {
    const auto currentDay = DateTimeProvider::GetFormattedDate();
    if (Name == currentDay) return;
    CloseLogFile();
    Name = currentDay;
    File.open(Name + ".log", std::ios::app);
    if (File.is_open()) return;
    throw std::runtime_error("Failed to open or create log file.");
}

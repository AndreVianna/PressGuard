#include "DateTimeProvider.h"

#include "LogHandler.h"
#include <iostream>
#include <stdexcept>

LogHandler::LogHandler()
    : LogHandler(2, new DateTimeProvider()) {
}

LogHandler::LogHandler(const int level)
    : LogHandler(level, new DateTimeProvider()) {
}

LogHandler::LogHandler(const int level, DateTimeProvider* dateTime)
    : Level(level)
    , DateTime(dateTime) {
    OpenOrCreateLogFile();
}

LogHandler::LogHandler(const LogHandler& other)
    : LogHandler(other.Level, other.DateTime) {
}

LogHandler::~LogHandler() {
    CloseLogFile();
}

LogHandler& LogHandler::operator=(const LogHandler& other) {
    if (this == &other) {
        return *this;
    }

    Level = other.Level;
    DateTime = other.DateTime;

    return *this;
}

int LogHandler::GetLogLevel() const
{
    return Level;
}

void LogHandler::LogDebug(const string& message) {
    if (Level > 0) return;
    Log(string("DEBUG"), message);
}

void LogHandler::LogInfo(const string& message) {
    if (Level > 1) return;
    Log("INFO", message);
}

void LogHandler::LogWarning(const string& message) {
    if (Level > 2) return;
    Log("WARN", message);
}

void LogHandler::LogError(const string& message) {
    if (Level > 3) return;
    Log("ERROR", message);
}

void LogHandler::Log(const string& level, const string& message) {
    OpenOrCreateLogFile();
    const auto output = DateTime->GetFormattedTime() + ": [" + level + "] " + message;
    File << output << "\n" << flush;
    File.flush();
    cerr << output << endl;
}

void LogHandler::CloseLogFile() {
    if (!File.is_open()) return;
    File.close();
}

void LogHandler::OpenOrCreateLogFile() {
    const auto currentDay = DateTime->GetFormattedDate();
    if (Name == currentDay) return;
    CloseLogFile();
    Name = currentDay;
    File.open(Name + ".log", ios::app);
    if (File.is_open()) return;
    throw runtime_error("Failed to open or create log file.");
}

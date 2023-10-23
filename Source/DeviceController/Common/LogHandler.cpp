#include "DateTimeFormatter.h"

#include "LogHandler.h"
#include <iostream>
#include <stdexcept>

LogHandler::LogHandler(const int level, DateTimeFormatter* dateTime, File* file)
    : _level(level),
    _dateTime(dateTime),
    _file(file) {
    OpenOrCreateLogFile();
}

LogHandler::~LogHandler() {
    CloseLogFile();
}

int LogHandler::GetLogLevel() const  {
    return _level;
}

void LogHandler::LogDebug(const string& message) {
    if (_level > 0) return;
    Log(string("DEBUG"), message);
}

void LogHandler::LogInfo(const string& message) {
    if (_level > 1) return;
    Log("INFO", message);
}

void LogHandler::LogWarning(const string& message) {
    if (_level > 2) return;
    Log("WARN", message);
}

void LogHandler::LogError(const string& message) {
    if (_level > 3) return;
    Log("ERROR", message);
}

void LogHandler::Log(const string& level, const string& message) {
    OpenOrCreateLogFile();
    const auto output = _dateTime->GetTime() + ": [" + level + "] " + message;
    _file->AppendLine(output);
    cerr << output << endl;
}

void LogHandler::CloseLogFile() const {
    _file->Close();
}

void LogHandler::OpenOrCreateLogFile() {
    const auto currentDay = _dateTime->GetDate();
    if (_name == currentDay) return;
    CloseLogFile();
    _name = currentDay;
    _file->Open(_name + ".log");
    if (_file->IsOpen()) return;
    throw runtime_error("Failed to open or create log file.");
}

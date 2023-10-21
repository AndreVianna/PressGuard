// FileHandler.cpp

#include "FileHandler.h"
#include <iostream>
#include <ctime>
#include <sstream>
#include <stdexcept>

FileHandler::FileHandler() : file(), currentDay() {}

FileHandler FileHandler::OpenOrCreateFile() {
    FileHandler handler;

    // Get the current day in sortable format
    std::time_t t = std::time(nullptr);
    std::tm* timePtr = std::gmtime(&t);
    if (timePtr == nullptr) {
        throw std::runtime_error("Failed to get the current time.");
    }

    char buffer[11]; // YYYYMMDD + null terminator
    snprintf(buffer, sizeof(buffer), "%04d%02d%02d", timePtr->tm_year + 1900, timePtr->tm_mon + 1, timePtr->tm_mday);
    handler.currentDay = std::string(buffer);

    // Open or create the file for the current day
    handler.file.open(handler.currentDay + ".dat", std::ios::app);
    if (!handler.file.is_open()) {
        throw std::runtime_error("Failed to open or create the file.");
    }

    return handler;
}

void FileHandler::AppendLine(const std::string& message) {
    if (!file.is_open()) {
        throw std::runtime_error("File is not open.");
    }

    file << message << std::endl;
}


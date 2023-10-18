// DataFileHandler.cpp

#include "DataFileHandler.h"
#include "DateTimeProvider.h"
#include <iostream>
#include <stdexcept>

DataFileHandler::DataFileHandler() = default;

DataFileHandler DataFileHandler::CreateFile() {
    DataFileHandler handler;
    handler.OpenOrCreateFile();
    return handler;
}

std::string DataFileHandler::AppendLine(const std::string& message) {
    if (const auto currentDay = DateTimeProvider::GetFormattedDate(); Name != currentDay) {
        CloseFile();
        Name = currentDay;
        OpenOrCreateFile();
    }
    if (!File.is_open()) {
        throw std::runtime_error("Data file is not open.");
    }

    auto output = DateTimeProvider::GetFormattedTime() + "," + message;
    File << output << "\n" << std::flush;
    return { output };
}

void DataFileHandler::CloseFile() {
    if (File.is_open()) {
        File.close();
    }
}

void DataFileHandler::OpenOrCreateFile() {
    File.open(Name + ".dat", std::ios::app);
    if (!File.is_open()) {
        throw std::runtime_error("Failed to open or create data file.");
    }

}

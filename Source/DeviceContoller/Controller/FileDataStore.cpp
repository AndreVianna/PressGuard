// FileDataStore.cpp

#include "DateTimeProvider.h"
#include "FileDataStore.h"

#include <iosfwd>
#include <iostream>
#include <stdexcept>

FileDataStore::FileDataStore() = default;

FileDataStore FileDataStore::Create() {
    FileDataStore handler;
    handler.OpenOrCreateFile();
    return handler;
}

std::string FileDataStore::Save(const std::string& message) {
    OpenOrCreateFile();
    auto output = DateTimeProvider::GetFormattedTime() + "," + message;
    File << output << "\n" << std::flush;
    return { output };
}

void FileDataStore::CloseFile() {
    if (File.is_open()) {
        File.close();
    }
}

void FileDataStore::OpenOrCreateFile() {
    if (const auto currentDay = DateTimeProvider::GetFormattedDate(); Name != currentDay) {
        CloseFile();
        Name = currentDay;
    }
    File.open(Name + ".dat", std::ios::app);
    if (!File.is_open()) {
        throw std::runtime_error("Failed to open or create data file.");
    }
}

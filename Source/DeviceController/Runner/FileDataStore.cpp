#include "IDateTimeProvider.h"
#include "FileDataStore.h"

#include <iosfwd>
#include <iostream>
#include <stdexcept>


FileDataStore::FileDataStore(IDateTimeProvider* dateTime) {
    DateTime = dateTime;
    OpenOrCreateFile();
}

FileDataStore::~FileDataStore() {
    CloseFile();
}

std::string FileDataStore::Save(const std::string& message) {
    OpenOrCreateFile();
    auto output = DateTime->GetFormattedTime() + "," + message;
    File << output << "\n" << std::flush;
    File.flush();
    return { output };
}

void FileDataStore::CloseFile() {
    if (!File.is_open()) return;
    File.close();
}

void FileDataStore::OpenOrCreateFile() {
    const auto currentDay = DateTime->GetFormattedDate();
    if (Name == currentDay) return;
    CloseFile();
    Name = currentDay;
    File.open(Name + ".dat", std::ios::app);
    if (File.is_open()) return;
    throw std::runtime_error("Failed to open or create data file.");
}

#include "FileDataStore.h"

FileDataStore::FileDataStore()
    : FileDataStore(new DateTimeProvider()) {
}

FileDataStore::FileDataStore(DateTimeProvider* dateTime)
    : DateTime(dateTime) {
    OpenOrCreateFile();
}

FileDataStore::FileDataStore(const FileDataStore& other)
    : FileDataStore(other.DateTime) {
}

FileDataStore& FileDataStore::operator=(const FileDataStore& other) {
    if (this == &other) {
        return *this;
    }

    DateTime = other.DateTime;
    return *this;
}

FileDataStore::~FileDataStore() {
    CloseFile();
}

string FileDataStore::Save(const string& message) {
    OpenOrCreateFile();
    auto output = DateTime->GetFormattedTime() + "," + message;
    File << output << "\n" << flush;
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
    File.open(Name + ".dat", ios::app);
    if (File.is_open()) return;
    throw runtime_error("Failed to open or create data file.");
}

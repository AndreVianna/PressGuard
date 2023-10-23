#pragma once

#include <fstream>
#include <iosfwd>
#include "../Common/DateTimeProvider.h"

using namespace std;

class FileDataStore {
public:
    FileDataStore();
    explicit FileDataStore(DateTimeProvider* dateTime);
    FileDataStore(const FileDataStore& other);
    FileDataStore(FileDataStore&& other) noexcept = default;

    string Save(const string& message);

    FileDataStore& operator=(const FileDataStore& other);
    FileDataStore& operator=(FileDataStore&& other) noexcept = default;;
    ~FileDataStore();
private:
    void OpenOrCreateFile();
    void CloseFile();
    DateTimeProvider* DateTime;
    ofstream File;
    string Name;
};


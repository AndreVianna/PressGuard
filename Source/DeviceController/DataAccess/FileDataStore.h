#pragma once

#include <fstream>
#include <iosfwd>
#include "../Common/DateTimeFormatter.h"

using namespace std;

class FileDataStore {
public:
    FileDataStore();
    explicit FileDataStore(DateTimeFormatter* dateTime);
    FileDataStore(const FileDataStore& other);
    FileDataStore(FileDataStore&& other) noexcept = default;

    string Save(const string& message);

    FileDataStore& operator=(const FileDataStore& other);
    FileDataStore& operator=(FileDataStore&& other) noexcept = default;;
    ~FileDataStore();
private:
    void OpenOrCreateFile();
    void CloseFile();
    DateTimeFormatter* DateTime;
    ofstream File;
    string Name;
};


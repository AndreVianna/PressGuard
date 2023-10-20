#pragma once

#include "IDateTimeProvider.h"

#include <fstream>
#include <iosfwd>

class FileDataStore {
public:
    explicit FileDataStore(IDateTimeProvider* dateTime);
    std::string Save(const std::string& message);
    ~FileDataStore();
private:
    void OpenOrCreateFile();
    void CloseFile();
    IDateTimeProvider* DateTime;
    std::ofstream File;
    std::string Name;
};


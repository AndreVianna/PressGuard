#pragma once

#include <fstream>
#include <iosfwd>

class FileDataStore {
public:
    FileDataStore();
    std::string Save(const std::string& message);
    ~FileDataStore();
private:
    void OpenOrCreateFile();
    void CloseFile();
    std::ofstream File;
    std::string Name;
};


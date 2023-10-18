#pragma once

#include <fstream>
#include <iosfwd>

class FileDataStore {
public:
    static FileDataStore Create();
    std::string Save(const std::string& message);
private:
    FileDataStore();
    void OpenOrCreateFile();
    void CloseFile();
    std::ofstream File;
    std::string Name;
};


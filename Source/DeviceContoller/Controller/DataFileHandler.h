#pragma once

#include <fstream>
#include <string>

class DataFileHandler {
public:
    static DataFileHandler CreateFile();
    std::string AppendLine(const std::string& message);
private:
    DataFileHandler();
    void OpenOrCreateFile();
    void CloseFile();
    std::ofstream File;
    std::string Name;
};

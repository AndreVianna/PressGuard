#pragma once

#include <fstream>

using namespace std;

class File {
public:
    virtual void Open(string name) = 0;
    virtual bool IsOpen() = 0;
    virtual void Close() = 0;
    virtual void AppendLine(string data) = 0;
    virtual ~File() = default;
};

class SystemFile final : public File {
public:
    void Open(const string name) override { 
        if (IsOpen()) return;
        _file.open(name, ios::app);
    }
    
    bool IsOpen() override { 
        return _file.is_open();
    }
    
    void Close() override { 
        if (!IsOpen()) return;
        _file.close();
    }
    
    void AppendLine(const string data) override {
        if (!IsOpen()) return;
        _file << data << "\n" << flush;
        _file.flush();
    }
    
    ~SystemFile() override {
         Close();
    }
private:
    fstream _file;
};
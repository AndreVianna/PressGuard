#pragma once
#include <string>

struct Messages {
    static std::string GetFormattedMessage(const std::string& code, const std::string& arg = "");
};

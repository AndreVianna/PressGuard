#pragma once

#include <string>

using namespace std;

struct Messages {
    static string GetFormattedMessage(const string& code, const string& arg = "");
};

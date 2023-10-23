#pragma once

#include <string>
using namespace std;

class DateTimeProvider {
public:
    string GetFormattedTime();
    string GetFormattedDate();
};

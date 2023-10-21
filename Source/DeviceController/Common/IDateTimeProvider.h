#pragma once

#include <string>

class IDateTimeProvider {
public:
    virtual std::string GetFormattedTime();
    virtual std::string GetFormattedDate();
    virtual ~IDateTimeProvider() = default;
};

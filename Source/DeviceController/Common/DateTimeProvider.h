#pragma once

#include "IDateTimeProvider.h"

#include <chrono>
#include <string>

class DateTimeProvider : public IDateTimeProvider {
public:
    std::string GetFormattedTime() override;
    std::string GetFormattedDate() override;
    ~DateTimeProvider() override = default;
private:
    static tm* TheadSafeUtcNow(const std::chrono::time_point<std::chrono::system_clock>& now);
};

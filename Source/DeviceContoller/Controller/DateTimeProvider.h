#pragma once

#include <chrono>
#include <string>

class DateTimeProvider {
public:
    static std::string GetFormattedTime();
    static std::string GetFormattedDate();
private:
    static tm* TheadSafeUtcNow(const std::chrono::time_point<std::chrono::system_clock>& now);
};

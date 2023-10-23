#include "DateTimeProvider.h"

#include <chrono>

struct DateTime {
    int Year;
    int Month;
    int Day;
    int Hour;
    int Minutes;
    int Seconds;
    int Milliseconds;
};

DateTime GetCurrentDateTime() {
    using namespace chrono;
    const auto now = system_clock::now();
    const auto tt = system_clock::to_time_t(now);
    tm tm;
    gmtime_r(&tt, &tm);
    const auto ms = (duration_cast<milliseconds>(now.time_since_epoch()) % 1000).count();
    const DateTime dateTime{
        .Year = tm.tm_year + 1900,
        .Month = tm.tm_mon + 1,
        .Day = tm.tm_mday,
        .Hour = tm.tm_hour,
        .Minutes = tm.tm_min,
        .Seconds = tm.tm_sec,
        .Milliseconds = static_cast<int>(ms)
    };
    return dateTime;
}

string DateTimeProvider::GetFormattedDate() {
    const auto [Year, Month, Day, Hour, Minutes, Seconds, Milliseconds] = GetCurrentDateTime();
    char buffer[10];
    sprintf(buffer, "%04d%02d%02d", (1900 + Year), Month, Day);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

string DateTimeProvider::GetFormattedTime() {
    const auto [Year, Month, Day, Hour, Minutes, Seconds, Milliseconds] = GetCurrentDateTime();
    char buffer[14];
    sprintf(buffer, "%02d:%02d:%02d.%03d", Hour, Minutes, Seconds, Milliseconds);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

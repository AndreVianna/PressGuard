#include "DateTimeFormatter.h"

#include <chrono>

DateTime DateTimeProvider::Now() {
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

string DateTimeFormatter::GetDate() const {
    const auto [Year, Month, Day, Hour, Minutes, Seconds, Milliseconds] = _dateTime->Now();
    char buffer[10];
    sprintf(buffer, "%04d%02d%02d", Year, Month, Day);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

string DateTimeFormatter::GetTime() const {
    const auto [Year, Month, Day, Hour, Minutes, Seconds, Milliseconds] = _dateTime->Now();
    char buffer[14];
    sprintf(buffer, "%02d:%02d:%02d.%03d", Hour, Minutes, Seconds, Milliseconds);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

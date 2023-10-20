#include "DateTimeProvider.h"
#include <chrono>
#include <ctime>
#include <mutex>

std::mutex mutexLock;

tm* DateTimeProvider::TheadSafeUtcNow(const std::chrono::time_point<std::chrono::system_clock>& now)
{
    const auto timeNow = std::chrono::system_clock::to_time_t(now);
    std::lock_guard lock(mutexLock);
    return std::gmtime(&timeNow);  // NOLINT(concurrency-mt-unsafe) - used lock_guard to make this thread safe.
}

std::string DateTimeProvider::GetFormattedDate() {
    const auto tm = TheadSafeUtcNow(std::chrono::system_clock::now());

    char buffer[10];
    sprintf(buffer, "%04d%02d%02d", (1900 + tm->tm_year), tm->tm_mon, tm->tm_mday);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

std::string DateTimeProvider::GetFormattedTime() {
    const auto now = std::chrono::system_clock::now();
    const auto tm = TheadSafeUtcNow(now);
    const auto milliseconds = (std::chrono::duration_cast<std::chrono::milliseconds>(now.time_since_epoch()) % 1000).count();

    char buffer[14];
    sprintf(buffer, "%02d:%02d:%02d.%03lld", tm->tm_hour, tm->tm_min, tm->tm_sec, milliseconds);  // NOLINT(cert-err33-c) - No need to check return size here.
    return { buffer };
}

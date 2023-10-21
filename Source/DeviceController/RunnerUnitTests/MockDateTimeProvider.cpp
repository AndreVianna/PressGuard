#include "MockDateTimeProvider.h"

#include <utility>

MockDateTimeProvider::MockDateTimeProvider(const std::string& mockedDate, const std::string& mockedTime)
    : MockedDate(mockedDate)
    , MockedTime(mockedTime) {
}

std::string MockDateTimeProvider::GetFormattedDate() {
    return MockedDate;
}

std::string MockDateTimeProvider::GetFormattedTime() {
    return MockedTime;
}

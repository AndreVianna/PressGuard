#pragma once

#include "IDateTimeProvider.h"

class MockDateTimeProvider : public IDateTimeProvider {
public:
    MockDateTimeProvider(const std::string& mockedDate, const std::string& mockedTime);
    std::string GetFormattedTime() override;
    std::string GetFormattedDate() override;
    ~MockDateTimeProvider() override = default;
private:
    std::string MockedDate;
    std::string MockedTime;
};
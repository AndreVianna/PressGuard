#include "../Common/DateTimeFormatter.h"

#include <cstddef>
#include <gtest/gtest.h>
#include <regex>

class MockDateTimeProvider : public DateTimeProvider {
public:
    DateTime Now() override {
        auto fixedDateTime = DateTime{
            .Year = 2021,
            .Month = 6,
            .Day = 28,
            .Hour = 12,
            .Minutes = 34,
            .Seconds = 56,
            .Milliseconds = 123,
        };
        return { fixedDateTime };
    }
};

TEST(DateTimeFormatterTests, DefaultConstructor_Returns) {
    const DateTimeFormatter formatter{};
    const auto time = formatter.GetTime();
    const auto date = formatter.GetDate();

    ASSERT_TRUE(std::regex_match(time, std::regex("\\d{2}:\\d{2}:\\d{2}\\.\\d{3}")));
    ASSERT_TRUE(std::regex_match(date, std::regex("\\d{4}\\d{2}\\d{2}")));
}

TEST(DateTimeFormatterTests, GetTime_ReturnsFormattedString) {
    MockDateTimeProvider mockDateTime{};
    const DateTimeFormatter formatter(&mockDateTime);
    const auto time = formatter.GetTime();
    ASSERT_EQ("12:34:56.123", time);
}

TEST(DateTimeFormatterTests, GetDate_ReturnsFormattedString) {
    MockDateTimeProvider mockDateTime{};
    const DateTimeFormatter formatter(&mockDateTime);
    const auto date = formatter.GetDate();
    ASSERT_EQ("20210628", date);
}

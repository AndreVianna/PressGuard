#pragma once

#include <string>
using namespace std;

struct DateTime {
    int Year;
    int Month;
    int Day;
    int Hour;
    int Minutes;
    int Seconds;
    int Milliseconds;
};

class DateTimeProvider {
public:
    virtual DateTime Now();
};

class DateTimeFormatter {
public:
    DateTimeFormatter() : DateTimeFormatter(new DateTimeProvider()) { }
    explicit DateTimeFormatter(DateTimeProvider* timeProvider) : _dateTime(timeProvider) {}
    [[nodiscard]] string GetDate() const;
    [[nodiscard]] string GetTime() const;

private:
    DateTimeProvider* _dateTime;
};

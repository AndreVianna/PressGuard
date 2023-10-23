#include "../Common/LogHandler.h"
#include "../Common/DateTimeFormatter.h"
#include <gtest/gtest.h>
#include <string>

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

class MockFile : public File {
public:
    void Open(string name) override {
        opened = true;
    }
    bool IsOpen() override {
        return opened;
    }
    void Close() override {
        opened = false;
    }
    void AppendLine(string data) override {
        lines.push_back(data);
    }
    std::vector<string> lines;
    bool opened = false;
};

TEST(LogHandlerTests, DefaultConstructor_SetsDefaultLevel) {
    LogHandler logger;
    ASSERT_EQ(2, logger.GetLogLevel());
}

TEST(LogHandlerTests, ParameterizedConstructor_SetsLevel) {
    LogHandler logger(3);
    ASSERT_EQ(3, logger.GetLogLevel());
}

TEST(LogHandlerTests, LogDebug_LogsWhenLevelIsZero) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(0, &formatter, &mockFile);

    logger.LogDebug("Debug message");
    ASSERT_EQ(1, mockFile.lines.size());
    ASSERT_EQ("12:34:56.123: [DEBUG] Debug message", mockFile.lines[0]);
}

TEST(LogHandlerTests, LogInfo_LogsWhenLevelIsOneOrLower) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(1, &formatter, &mockFile);

    logger.LogInfo("Information message");
    ASSERT_EQ(1, mockFile.lines.size());
    ASSERT_EQ("12:34:56.123: [INFO] Information message", mockFile.lines[0]);
}


TEST(LogHandlerTests, LogWarning_LogsWhenLevelIsTwoOrLower) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(2, &formatter, &mockFile);

    logger.LogWarning("Warning message");
    ASSERT_EQ(1, mockFile.lines.size());
    ASSERT_EQ("12:34:56.123: [WARN] Warning message", mockFile.lines[0]);
}

TEST(LogHandlerTests, LogError_LogsWhenLevelIsThreeOrLower) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(3, &formatter, &mockFile);

    logger.LogError("Error message");
    ASSERT_EQ(1, mockFile.lines.size());
    ASSERT_EQ("12:34:56.123: [ERROR] Error message", mockFile.lines[0]);
}

TEST(LogHandlerTests, LogDebug_DoesNotLogWhenLevelIsOneOrHigher) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(1, &formatter, &mockFile);

    logger.LogDebug("No message");
    ASSERT_EQ(0, mockFile.lines.size());
}

TEST(LogHandlerTests, LogInfo_DoesNotLogWhenLevelIsTwoOrHigher) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(2, &formatter, &mockFile);

    logger.LogInfo("No message");
    ASSERT_EQ(0, mockFile.lines.size());
}

TEST(LogHandlerTests, LogWarning_DoesNotLogWhenLevelIsThreeOrHigher) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(3, &formatter, &mockFile);

    logger.LogWarning("No message");
    ASSERT_EQ(0, mockFile.lines.size());
}

TEST(LogHandlerTests, LogError_DoesNotLogWhenLevelIsFourOrHigher) {
    MockDateTimeProvider mockDateTime{};
    DateTimeFormatter formatter(&mockDateTime);
    MockFile mockFile;
    LogHandler logger(4, &formatter, &mockFile);

    logger.LogError("No message");
    ASSERT_EQ(0, mockFile.lines.size());
}

#include "DeviceHandler.h"
#include <gtest/gtest.h>

TEST(DeviceHandlerTests, PublicConstants_HaveTheExpectedValues) {
    ASSERT_EQ(8, DeviceHandler::DEFAULT_NUMBER_OF_SENSORS);
    ASSERT_EQ(500000, DeviceHandler::DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS);
}

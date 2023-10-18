#include "pch.h"

#include "../Controller/DeviceHandler.h"
#include "CppUnitTest.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace DeviceControllerTests {
    TEST_CLASS(DeviceHandlerTests) {
public:

        TEST_METHOD(PublicConstatns_HaveTheExpectedValues) {
            Assert::AreEqual(8, DeviceHandler::DEFAULT_NUMBER_OF_SENSORS);
            Assert::AreEqual(500000, DeviceHandler::DEFAULT_DELAY_BETWEEN_SCANS_IN_MILLISECONDS);
        }
    };
}
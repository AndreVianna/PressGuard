#pragma once

#include "IDeviceHandler.h"
#include "MockDateTimeProvider.h"

class MockDeviceHandler : public IDeviceHandler {
public:
    MockDeviceHandler();
    void ProcessSignals() const override;
    ~MockDeviceHandler() override = default;
private:
    MockDateTimeProvider* DateTime;
};
#pragma once

class IDeviceHandler {
public:
    virtual void ProcessSignals() const = 0;
    virtual ~IDeviceHandler() = default;
};
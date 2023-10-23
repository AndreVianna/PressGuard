#include "Messages.h"

std::string Messages::GetFormattedMessage(const std::string& code, const std::string& arg) {
    if (code == "InitFail") return "Failed to initialize device. Result: " + arg;
    if (code == "SensorRead") return "Sensor " + arg + ": ";
    if (code == "SensorFail") return "Failed to read sensor " + arg + ".";
    return "Unknown message code.";
}

using RemoteService.Models;

namespace RemoteService.Handlers.Setting;

public record SettingRow : Row {
    public required string Name { get; init; }
}
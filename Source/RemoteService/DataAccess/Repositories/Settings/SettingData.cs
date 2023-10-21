namespace RemoteService.Repositories.Settings;

public record SettingData : Persisted {
    public string? ShortName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }
    public required AttributeDefinitionData[] AttributeDefinitions { get; init; }

    public record AttributeDefinitionData {
        public string? ShortName { get; init; }
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string DataType { get; init; }
    }
}

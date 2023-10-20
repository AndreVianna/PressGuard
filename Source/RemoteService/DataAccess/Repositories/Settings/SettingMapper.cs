namespace DataAccess.Repositories.Settings;

public static class SettingMapper {
    public static SettingData ToData(this Setting input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),
            AttributeDefinitions = input.AttributeDefinitions.Select(ToData).ToArray(),

            ChangeStamp = input.ChangeStamp,
        };

    public static SettingRow ToRow(this SettingData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static Setting? ToModel(this SettingData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
                Tags = input.Tags,
                AttributeDefinitions = input.AttributeDefinitions.Select(ToModel).ToArray(),

                ChangeStamp = input.ChangeStamp,
            };

    private static SettingData.AttributeDefinitionData ToData(IAttributeDefinition input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = input.DataType.GetName(),
        };

    private static AttributeDefinition ToModel(SettingData.AttributeDefinitionData input)
        => new() {
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            DataType = Create.TypeFrom(input.DataType),
        };
}

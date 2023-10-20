using GameSystem = RemoteService.Handlers.System.System;

namespace DataAccess.Repositories.Systems;

public static class SystemMapper {
    public static SystemData ToData(GameSystem input)
        => new() {
            Id = input.Id,
            State = input.State,
            ShortName = input.ShortName,
            Name = input.Name,
            Description = input.Description,
            Tags = input.Tags.ToArray(),

            ChangeStamp = input.ChangeStamp,
        };

    public static SystemRow ToRow(SystemData input)
        => new() {
            Id = input.Id,
            Name = input.Name,
        };

    public static GameSystem? ToModel(SystemData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,
                State = input.State,
                ShortName = input.ShortName,
                Name = input.Name,
                Description = input.Description,
                Tags = input.Tags,

                ChangeStamp = input.ChangeStamp,
            };
}

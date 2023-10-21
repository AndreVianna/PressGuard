using RemoteService.Handlers.System;

using GameSystem = RemoteService.Handlers.System.System;

namespace RemoteService.Controllers.Systems.Models;

internal static class SystemMapper {
    public static SystemRowResponse[] ToResponse(this IEnumerable<SystemRow> rows)
        => rows.Select(ToResponse).ToArray();

    private static SystemRowResponse ToResponse(this SystemRow row)
        => new() {
            Id = (Base64Guid)row.Id,
            Name = row.Name
        };

    public static SystemResponse ToResponse(this GameSystem model)
        => new() {
            Id = (Base64Guid)model.Id,
            Name = model.Name,
            Description = model.Description,
            ShortName = model.ShortName,
            Tags = model.Tags,
        };

    public static GameSystem ToDomain(this SystemRequest request, Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ShortName = request.ShortName,
            Tags = request.Tags,
        };
}

using RemoteService.Handlers.Tenants;
using RemoteService.Models;

namespace RemoteService.Controllers.Tenants.Models;

internal static class TenantMapper {
    public static TenantRowResponse[] ToResponse(this IEnumerable<TenantRow> rows)
        => rows.Select(ToResponse).ToArray();

    private static TenantRowResponse ToResponse(this TenantRow row)
        => new() {
            Id = (Base64Guid)row.Id,
            Name = row.Name
        };

    public static TenantResponse ToResponse(this Tenant model)
        => new() {
            Id = (Base64Guid)model.Id,
            Name = model.Name,
            Description = model.Description,
        };

    public static Tenant ToDomain(this TenantRequest request, Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
        };
}

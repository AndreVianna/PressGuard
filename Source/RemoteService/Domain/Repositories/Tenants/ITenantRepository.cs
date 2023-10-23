using RemoteService.Handlers.Tenants;

namespace RemoteService.Repositories.Tenants;

public interface ITenantRepository
    : IRepository<Tenant, TenantRow> {
}

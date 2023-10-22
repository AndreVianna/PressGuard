using RemoteService.Repositories.Tenants;

namespace RemoteService.Handlers.Tenants;

public class TenantHandler
    : CrudHandler<Tenant, TenantRow, ITenantRepository>,
      ITenantHandler {
    public TenantHandler(ITenantRepository repository)
        : base(repository) {
    }
}

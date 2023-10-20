using RemoteService.Repositories.System;

namespace RemoteService.Handlers.System;

public class SystemHandler
    : CrudHandler<System, SystemRow, ISystemRepository>,
      ISystemHandler {
    public SystemHandler(ISystemRepository repository)
        : base(repository) {
    }
}

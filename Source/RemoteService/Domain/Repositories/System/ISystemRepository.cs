﻿using RemoteService.Handlers.System;

namespace RemoteService.Repositories.System;

public interface ISystemRepository
    : IRepository<Handlers.System.System, SystemRow> {
}
﻿using Repository.Contracts;

using Application = Repository.PostgreSql.Schema.Application;

namespace Repository.PostgreSql;

internal sealed class PostgreSqlLocalizationRepositoryFactory : ILocalizationRepositoryFactory {
    private readonly Application _application;
    private readonly LocalizationDbContext _dbContext;
    private static readonly ConcurrentDictionary<Guid, Application> _applications = new();

    public PostgreSqlLocalizationRepositoryFactory(LocalizationDbContext dbContext, LocalizationRepositoryOptions options) {
        _dbContext = dbContext;
        _application = _applications.GetOrAdd(options.ApplicationId, id
            => _dbContext.Applications.FirstOrDefault(a => a.Id == id)
            ?? throw new NotSupportedException($"An application with id '{id}' was not found."));
    }

    public ILocalizationRepository CreateFor(string culture)
        => _application.AvailableCultures.Contains(culture)
            ? new PostgreSqlLocalizationRepository(_dbContext, _application, culture)
            : throw new NotSupportedException($"Culture '{culture}' is not available for application '{_application.Name}'.");
}

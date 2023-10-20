﻿using Repository.Contracts;

namespace Repository;

internal class ApplicationHandler {
    private readonly IApplicationRepository _repository;

    public ApplicationHandler(IApplicationRepository repository) {
        _repository = repository;
    }

    public CrudResult<IEnumerable<Application>> ListApplications()
        => _repository.ListApplications().ToArray();

    public CrudResult<Application> FindApplicationById(Guid id)
        => _repository.FindApplicationById(id)
        ?? CrudResult.NotFound<Application>();

    public CrudResult<Application> AddApplication(Application application) {
        var result = application.Validate();
        if (result.IsInvalid)
            return CrudResult.Invalid(application, result.Errors);
        var isSuccess = _repository.AddApplication(application);
        return isSuccess
            ? application
            : CrudResult.Conflict(application);
    }

    public CrudResult<Application> UpdateApplication(Application application) {
        var result = application.Validate();
        if (result.IsInvalid)
            return CrudResult.Invalid(application, result.Errors);
        var isSuccess = _repository.UpdateApplication(application);
        return isSuccess
            ? application
            : CrudResult.NotFound<Application>();
    }

    public CrudResult RemoveApplication(Guid id) {
        _repository.RemoveApplication(id);
        return CrudResult.Success();
    }
}

namespace Repository.Contracts;

public record LocalizationRepositoryOptions {
    [Required]
    public required Guid ApplicationId { get; init; }
};

using RemoteService.Constants;

namespace RemoteService.Models;

public record Base : IBase, IValidatable {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();

    public virtual Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Name.IsRequired()
                      .And().IsNotEmptyOrWhiteSpace()
                      .And().LengthIsAtMost(Validation.Name.MaximumLength).Result;
        result += Description.IsRequired()
                             .And().IsNotEmptyOrWhiteSpace()
                             .And().LengthIsAtMost(Validation.Description.MaximumLength).Result;
        result += ShortName.IsOptional()
                           .And().IsNotEmptyOrWhiteSpace()
                           .And().LengthIsAtMost(Validation.ShortName.MaximumLength).Result;
        result += Tags!.CheckIfEach(item =>
                                        item.IsRequired()
                                            .And().IsNotEmptyOrWhiteSpace()
                                            .And().LengthIsAtMost(Validation.Tag.MaximumLength)).Result;
        return result;
    }
}

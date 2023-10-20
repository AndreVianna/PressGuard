namespace Repository.Contracts;

public interface ILocalizationRepositoryFactory {
    ILocalizationRepository CreateFor(string culture);
}

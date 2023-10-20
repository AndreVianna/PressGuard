namespace Repository.Contracts;

public interface ITextRepository {
    LocalizedText? FindTextByKey(string textKey);
    void AddOrUpdateText(LocalizedText input);
}
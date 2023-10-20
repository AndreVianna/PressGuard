using static System.Text.Json.JsonSerializer;

namespace RemoteService.Repositories;

public partial class JsonFileStorage<TData> : IJsonFileStorage<TData>
    where TData : Persisted {
    private readonly ILogger<JsonFileStorage<TData>> _logger;
    private readonly FileSystemAccessor _io;
    private readonly DateTimeProvider _dateTime;

    private string _repositoryPath;

    private const string _timestampFormat = "yyyyMMddHHmmss";
    private const string _baseFolderKey = $"{nameof(JsonFileStorage<TData>)}:BaseFolder";

    public JsonFileStorage(IConfiguration configuration, FileSystemAccessor io, DateTimeProvider dateTime, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger<JsonFileStorage<TData>>() ?? NullLogger<JsonFileStorage<TData>>.Instance;
        _io = io;
        _dateTime = dateTime;
        _repositoryPath = Ensure.IsNotNullOrWhiteSpace(configuration[_baseFolderKey], $"{nameof(configuration)}[{_baseFolderKey}]").Trim();
        _io.CreateFolderIfNotExists(_repositoryPath);
    }

    public void SetBasePath(string path) {
        _repositoryPath = _io.CombinePath(_repositoryPath, path);
        _io.CreateFolderIfNotExists(_repositoryPath);
    }

    public async Task<IEnumerable<TData>> GetAllAsync(Func<TData, bool>? filter = null, CancellationToken cancellation = default) {
        try {
            _logger.LogDebug("Getting files from '{path}'...", _repositoryPath);
            var files = GetActiveFiles();
            var data = new List<TData>();
            foreach (var file in files) {
                var content = await GetFileDataOrDefaultAsync(file, cancellation).ConfigureAwait(false);
                if (content is null) continue;
                data.Add(content);
            }

            if (filter is not null)
                data = data.Where(filter).ToList();

            _logger.LogDebug("{fileCount} files retrieved from '{path}'.", data.Count, _repositoryPath);
            return data.ToArray();
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Failed to get files from '{path}'!", _repositoryPath);
            throw;
        }
    }

    public async Task<TData?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        try {
            _logger.LogDebug("Getting latest data from '{path}/{id}'...", _repositoryPath, id);
            var file = GetActiveFile(id);
            if (file is null) {
                _logger.LogDebug("File '{path}/{id}' not found.", _repositoryPath, id);
                return default;
            }

            return await GetFileDataOrDefaultAsync(file, cancellation).ConfigureAwait(false);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Failed to get data from file '{path}/{id}'!", _repositoryPath, id);
            throw;
        }
    }

    public async Task<TData?> CreateAsync(TData data, CancellationToken cancellation = default) {
        try {
            _logger.LogDebug("Adding data in '{path}/{id}'...", _repositoryPath, data.Id);
            var filePath = GetActiveFile(data.Id);
            if (filePath is not null) {
                _logger.LogDebug("File '{path}/{id}' already exists.", _repositoryPath, data.Id);
                return default;
            }

            data = data with { ChangeStamp = _dateTime.Now };
            filePath = await WriteToFileAsync("added", data, cancellation).ConfigureAwait(false);
            return await GetFileDataAsync(filePath, cancellation).ConfigureAwait(false);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Failed to add file '{path}/{id}'!", _repositoryPath, data.Id);
            throw;
        }
    }

    public async Task<TData?> UpdateAsync(TData data, CancellationToken cancellation = default) {
        try {
            _logger.LogDebug("Updating data in '{path}/{id}'...", _repositoryPath, data.Id);
            var filePath = GetActiveFile(data.Id);
            if (filePath is null) {
                _logger.LogDebug("File '{path}/{id}' not found.", _repositoryPath, data.Id);
                return default;
            }

            if (!TryGetFileMetadata(filePath, out var dateTime) || dateTime != data.ChangeStamp) {
                _logger.LogDebug("File '{path}/{id}' changed by another process.", _repositoryPath, data.Id);
                return default;
            }

            _io.MoveFile(filePath, filePath.Replace("+", ""));
            data = data with { ChangeStamp = _dateTime.Now };
            await WriteToFileAsync("updated", data, cancellation).ConfigureAwait(false);
            return data;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Failed to add or update file '{path}/{id}'!", _repositoryPath, data.Id);
            throw;
        }
    }

    private async Task<string> WriteToFileAsync(string operation,TData data, CancellationToken cancellation) {
        var filePath = _io.CombinePath(_repositoryPath, $"+{data.Id}_{_dateTime.Now:yyyyMMddHHmmss}.json");
        await using var stream = _io.CreateNewFileAndOpenForWriting(filePath);
        await SerializeAsync(stream, data, cancellationToken: cancellation);
        _logger.LogDebug("File '{filePath}' {operation}.", filePath, operation);
        return filePath;
    }

    public bool Delete(Guid id) {
        try {
            _logger.LogDebug("Deleting file '{path}/{id}'...", _repositoryPath, id);
            var filePath = GetActiveFile(id);
            if (filePath is null) {
                _logger.LogDebug("File '{path}/{id}' not found.", _repositoryPath, id);
                return false;
            }

            _io.MoveFile(filePath, filePath.Replace("+", "-"));

            _logger.LogDebug("File '{path}/{id}' deleted.", _repositoryPath, id);
            return true;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Failed to delete file '{path}/{id}'!", _repositoryPath, id);
            throw;
        }
    }

    private async Task<TData?> GetFileDataOrDefaultAsync(string filePathWithName, CancellationToken cancellation) {
        try {
            return await GetFileDataAsync(filePathWithName, cancellation);
        }
        catch (Exception ex) {
            _logger.LogWarning(ex, "File '{filePath}' content is invalid.", filePathWithName);
            return default;
        }
    }

    private async Task<TData> GetFileDataAsync(string filePathWithName, CancellationToken cancellation) {
        if (!TryGetFileMetadata(filePathWithName, out var changeStamp))
            throw new InvalidOperationException($"File name '{filePathWithName}' is invalid.");

        await using var stream = _io.OpenFileForReading(filePathWithName);
        var content = await DeserializeAsync<TData>(stream, cancellationToken: cancellation);
        content = content! with { ChangeStamp = changeStamp };
        _logger.LogDebug("Data from '{filePath}' retrieved.", filePathWithName);
        return content;
    }

    private bool TryGetFileMetadata(string filePathWithName, out DateTime dateTime) {
        var fileName = _io.GetFileNameFrom(filePathWithName);
        dateTime = _dateTime.Default;
        var match = FileNameMatcher().Match(fileName);
        if (!match.Success) return false;
        if (!_dateTime.TryParseExact(match.Groups["datetime"].Value, _timestampFormat, null, DateTimeStyles.None, out dateTime)) return false;
        return true;
    }

    private string? GetActiveFile(Guid id)
        => _io.GetFilesFrom(_repositoryPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
        .FirstOrDefault();

    private string[] GetActiveFiles()
        => _io.GetFilesFrom(_repositoryPath, "+*.json", SearchOption.TopDirectoryOnly);

    [GeneratedRegex(@"^\+(?<id>[a-zA-Z0-9-]{36})_(?<datetime>\d{14})\.json$", RegexOptions.Compiled, "en-CA")]
    private static partial Regex FileNameMatcher();
}

using RemoteService.Repositories.Setting;

namespace RemoteService.Handlers.Setting;

public class SettingHandler
    : CrudHandler<Setting, SettingRow, ISettingRepository>,
      ISettingHandler {
    public SettingHandler(ISettingRepository repository)
        : base(repository) {
    }
}
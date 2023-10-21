using RemoteService.Handlers.Setting;

namespace RemoteService.Repositories.Setting;

public interface ISettingRepository
    : IRepository<Handlers.Setting.Setting, SettingRow> {
}

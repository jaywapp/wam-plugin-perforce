using Wam.Core.Plugins;
using Wam.Core.Settings;

namespace Wam.Plugins.Perforce;

/// <summary>
/// Perforce settings page. Buffers edits like every ISettingsPage: the window
/// binds to the public properties, and Apply() (OK button) persists them through
/// the plugin settings store. Cancel discards — the VM is recreated per window.
/// </summary>
public sealed class PerforceSettingsViewModel : ISettingsPage
{
    private readonly IPluginSettingsStore _store;

    public PerforceSettingsViewModel(IPluginSettingsStore store)
    {
        _store = store;
        var s = store.Load<PerforceSettings>(PerforcePlugin.Id);
        Port = s.Port;
        User = s.User;
        Client = s.Client;
        ConnectionTimeoutSeconds = s.ConnectionTimeoutSeconds;
    }

    public string Port { get; set; }
    public string User { get; set; }
    public string Client { get; set; }
    public int ConnectionTimeoutSeconds { get; set; }

    public string Description =>
        "Perforce 서버 연결 정보를 설정합니다. 인증은 p4 login 티켓을 사용하며 비밀번호는 저장하지 않습니다.";

    public void Apply() =>
        _store.Save(PerforcePlugin.Id, new PerforceSettings
        {
            Port = Port.Trim(),
            User = User.Trim(),
            Client = Client.Trim(),
            ConnectionTimeoutSeconds = ConnectionTimeoutSeconds > 0 ? ConnectionTimeoutSeconds : 30,
        });
}

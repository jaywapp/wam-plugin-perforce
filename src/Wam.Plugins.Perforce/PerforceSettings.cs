namespace Wam.Plugins.Perforce;

/// <summary>
/// Connection settings for the Perforce plugin, persisted through
/// IPluginSettingsStore (never in the host's settings.json). No password field:
/// authentication relies on p4 login tickets — secrets are never stored.
/// </summary>
public sealed class PerforceSettings
{
    /// <summary>P4PORT, e.g. "ssl:p4.example.com:1666".</summary>
    public string Port { get; set; } = "";

    /// <summary>P4USER.</summary>
    public string User { get; set; } = "";

    /// <summary>P4CLIENT (workspace name).</summary>
    public string Client { get; set; } = "";

    public int ConnectionTimeoutSeconds { get; set; } = 30;
}

namespace Wam.Plugins.Perforce.Services;

/// <summary>
/// Boundary for the future p4 integration (depots, streams, changelists, sync).
/// Lives inside the plugin so the host never learns Perforce specifics; the
/// settings page and node actions will consume this once a real implementation
/// (p4 CLI or P4.NET) lands.
/// </summary>
public interface IPerforceClient
{
    /// <summary>Verifies the connection described by the settings (p4 info).</summary>
    Task<bool> TestConnectionAsync(PerforceSettings settings, CancellationToken ct = default);
}

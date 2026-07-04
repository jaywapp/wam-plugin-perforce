using Wam.Core.Nodes;
using Wam.Core.Plugins;
using Wam.Core.Relations;
using Wam.Core.ValueObjects;

namespace Wam.Plugins.Perforce;

/// <summary>
/// First official WAM plugin: Perforce node types, relations, and a settings page.
/// This stage establishes the plugin's shape (types + persisted settings); the
/// actual p4 server integration lands behind Services.IPerforceClient later.
/// </summary>
public sealed class PerforcePlugin : IWamPlugin
{
    public const string Id = "wam.plugin.perforce";

    public string PluginId => Id;
    public string DisplayName => "Perforce";
    public string Version => "0.1.0";

    public IEnumerable<INodeTypeDefinition> GetNodeTypes() =>
    [
        new NodeTypeDefinition
        {
            NodeTypeId = "perforce.depot",
            DisplayName = "Depot",
            DefaultColor = "#2563EB",
            Icon = "depot",
            DefaultViewMode = NodeViewMode.Simple,
        },
        new NodeTypeDefinition
        {
            NodeTypeId = "perforce.stream",
            DisplayName = "Stream",
            DefaultColor = "#0891B2",
            Icon = "stream",
            DefaultViewMode = NodeViewMode.Simple,
        },
        new NodeTypeDefinition
        {
            NodeTypeId = "perforce.workspace",
            DisplayName = "Workspace",
            DefaultColor = "#7C3AED",
            Icon = "workspace",
            DefaultViewMode = NodeViewMode.Normal,
        },
        new NodeTypeDefinition
        {
            NodeTypeId = "perforce.changelist",
            DisplayName = "Changelist",
            DefaultColor = "#EA580C",
            Icon = "changelist",
            DefaultViewMode = NodeViewMode.Normal,
            AvailableStatuses =
            [
                new StatusDescriptor("Pending",   "Pending",   "#F59E0B", 0),
                new StatusDescriptor("Shelved",   "Shelved",   "#8B5CF6", 1),
                new StatusDescriptor("Submitted", "Submitted", "#16A34A", 2),
            ],
            DefaultStatus = new StatusDescriptor("Pending", "Pending", "#F59E0B", 0)
        }
    ];

    public IEnumerable<RelationTypeDefinition> GetRelationTypes() =>
    [
        // Depot → Stream, Stream → Workspace (structure)
        new RelationTypeDefinition("perforce.contains", "Contains", RelationDirection.Directed),
        // Changelist → Stream (where the change lands)
        new RelationTypeDefinition("perforce.submitted-to", "Submitted To", RelationDirection.Directed),
    ];

    public IEnumerable<PluginSettingsPage> GetSettingsPages() =>
    [
        new PluginSettingsPage("perforce.settings", "Perforce", "Plugins", 10,
        [
            new PluginSettingField("port", "P4PORT", PluginSettingKind.Text,
                "Server address, e.g. ssl:p4.example.com:1666"),
            new PluginSettingField("user", "P4USER", PluginSettingKind.Text),
            new PluginSettingField("client", "P4CLIENT", PluginSettingKind.Text,
                "Workspace name"),
            new PluginSettingField("timeout", "Connection timeout (s)", PluginSettingKind.Integer,
                DefaultValue: "30"),
        ])
    ];
}

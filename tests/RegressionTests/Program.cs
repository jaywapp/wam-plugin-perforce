using Wam.Core.Plugins;
using Wam.Plugins.Perforce;
Check.That(PerforceSettings.From((PluginSettingsValues?)null).ConnectionTimeoutSeconds == 30, "Null settings");
foreach (var invalid in new[] { "", "invalid", "-1", "0", "999999999999" }) {
    var values = new PluginSettingsValues(new Dictionary<string,string> { ["timeout"] = invalid });
    Check.That(PerforceSettings.From(values).ConnectionTimeoutSeconds == 30, "Invalid timeout fallback");
}
var settings = PerforceSettings.From(new PluginSettingsValues(new Dictionary<string,string> { ["timeout"] = "90", ["port"] = "ssl:example.test:1666", ["user"] = "fixture", ["client"] = "workspace" }));
Check.That(settings.ConnectionTimeoutSeconds == 90 && settings.User == "fixture" && settings.Client == "workspace", "Valid settings");
Check.That(new PerforcePlugin().GetNodeTypes().Count() == 4, "Node registration");
Check.That(new PerforcePlugin().GetRelationTypes().Count() == 2, "Relation registration");
Console.WriteLine($"PASS {Check.Count} Perforce settings regression checks");

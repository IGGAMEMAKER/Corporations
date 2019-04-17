//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MetricsHistoryComponent metricsHistory { get { return (MetricsHistoryComponent)GetComponent(GameComponentsLookup.MetricsHistory); } }
    public bool hasMetricsHistory { get { return HasComponent(GameComponentsLookup.MetricsHistory); } }

    public void AddMetricsHistory(System.Collections.Generic.List<MetricsInfo> newMetrics) {
        var index = GameComponentsLookup.MetricsHistory;
        var component = (MetricsHistoryComponent)CreateComponent(index, typeof(MetricsHistoryComponent));
        component.Metrics = newMetrics;
        AddComponent(index, component);
    }

    public void ReplaceMetricsHistory(System.Collections.Generic.List<MetricsInfo> newMetrics) {
        var index = GameComponentsLookup.MetricsHistory;
        var component = (MetricsHistoryComponent)CreateComponent(index, typeof(MetricsHistoryComponent));
        component.Metrics = newMetrics;
        ReplaceComponent(index, component);
    }

    public void RemoveMetricsHistory() {
        RemoveComponent(GameComponentsLookup.MetricsHistory);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherMetricsHistory;

    public static Entitas.IMatcher<GameEntity> MetricsHistory {
        get {
            if (_matcherMetricsHistory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MetricsHistory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMetricsHistory = matcher;
            }

            return _matcherMetricsHistory;
        }
    }
}

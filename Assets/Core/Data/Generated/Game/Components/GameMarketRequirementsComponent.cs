//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MarketRequirementsComponent marketRequirements { get { return (MarketRequirementsComponent)GetComponent(GameComponentsLookup.MarketRequirements); } }
    public bool hasMarketRequirements { get { return HasComponent(GameComponentsLookup.MarketRequirements); } }

    public void AddMarketRequirements(System.Collections.Generic.List<float> newFeatures) {
        var index = GameComponentsLookup.MarketRequirements;
        var component = (MarketRequirementsComponent)CreateComponent(index, typeof(MarketRequirementsComponent));
        component.Features = newFeatures;
        AddComponent(index, component);
    }

    public void ReplaceMarketRequirements(System.Collections.Generic.List<float> newFeatures) {
        var index = GameComponentsLookup.MarketRequirements;
        var component = (MarketRequirementsComponent)CreateComponent(index, typeof(MarketRequirementsComponent));
        component.Features = newFeatures;
        ReplaceComponent(index, component);
    }

    public void RemoveMarketRequirements() {
        RemoveComponent(GameComponentsLookup.MarketRequirements);
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

    static Entitas.IMatcher<GameEntity> _matcherMarketRequirements;

    public static Entitas.IMatcher<GameEntity> MarketRequirements {
        get {
            if (_matcherMarketRequirements == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MarketRequirements);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMarketRequirements = matcher;
            }

            return _matcherMarketRequirements;
        }
    }
}

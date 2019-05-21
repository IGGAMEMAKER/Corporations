//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public NicheCostsComponent nicheCosts { get { return (NicheCostsComponent)GetComponent(GameComponentsLookup.NicheCosts); } }
    public bool hasNicheCosts { get { return HasComponent(GameComponentsLookup.NicheCosts); } }

    public void AddNicheCosts(float newBasePrice, long newClientBatch, int newTechCost, int newIdeaCost, int newMarketingCost, int newAdCost) {
        var index = GameComponentsLookup.NicheCosts;
        var component = (NicheCostsComponent)CreateComponent(index, typeof(NicheCostsComponent));
        component.BasePrice = newBasePrice;
        component.ClientBatch = newClientBatch;
        component.TechCost = newTechCost;
        component.IdeaCost = newIdeaCost;
        component.MarketingCost = newMarketingCost;
        component.AdCost = newAdCost;
        AddComponent(index, component);
    }

    public void ReplaceNicheCosts(float newBasePrice, long newClientBatch, int newTechCost, int newIdeaCost, int newMarketingCost, int newAdCost) {
        var index = GameComponentsLookup.NicheCosts;
        var component = (NicheCostsComponent)CreateComponent(index, typeof(NicheCostsComponent));
        component.BasePrice = newBasePrice;
        component.ClientBatch = newClientBatch;
        component.TechCost = newTechCost;
        component.IdeaCost = newIdeaCost;
        component.MarketingCost = newMarketingCost;
        component.AdCost = newAdCost;
        ReplaceComponent(index, component);
    }

    public void RemoveNicheCosts() {
        RemoveComponent(GameComponentsLookup.NicheCosts);
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

    static Entitas.IMatcher<GameEntity> _matcherNicheCosts;

    public static Entitas.IMatcher<GameEntity> NicheCosts {
        get {
            if (_matcherNicheCosts == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NicheCosts);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNicheCosts = matcher;
            }

            return _matcherNicheCosts;
        }
    }
}

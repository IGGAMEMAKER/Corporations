//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CEOComponent cEO { get { return (CEOComponent)GetComponent(GameComponentsLookup.CEO); } }
    public bool hasCEO { get { return HasComponent(GameComponentsLookup.CEO); } }

    public void AddCEO(float newReputation, int newHumanId) {
        var index = GameComponentsLookup.CEO;
        var component = (CEOComponent)CreateComponent(index, typeof(CEOComponent));
        component.Reputation = newReputation;
        component.HumanId = newHumanId;
        AddComponent(index, component);
    }

    public void ReplaceCEO(float newReputation, int newHumanId) {
        var index = GameComponentsLookup.CEO;
        var component = (CEOComponent)CreateComponent(index, typeof(CEOComponent));
        component.Reputation = newReputation;
        component.HumanId = newHumanId;
        ReplaceComponent(index, component);
    }

    public void RemoveCEO() {
        RemoveComponent(GameComponentsLookup.CEO);
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

    static Entitas.IMatcher<GameEntity> _matcherCEO;

    public static Entitas.IMatcher<GameEntity> CEO {
        get {
            if (_matcherCEO == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CEO);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCEO = matcher;
            }

            return _matcherCEO;
        }
    }
}
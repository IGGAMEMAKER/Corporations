//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public IndustryComponent industry { get { return (IndustryComponent)GetComponent(GameComponentsLookup.Industry); } }
    public bool hasIndustry { get { return HasComponent(GameComponentsLookup.Industry); } }

    public void AddIndustry(IndustryType newIndustryType) {
        var index = GameComponentsLookup.Industry;
        var component = (IndustryComponent)CreateComponent(index, typeof(IndustryComponent));
        component.IndustryType = newIndustryType;
        AddComponent(index, component);
    }

    public void ReplaceIndustry(IndustryType newIndustryType) {
        var index = GameComponentsLookup.Industry;
        var component = (IndustryComponent)CreateComponent(index, typeof(IndustryComponent));
        component.IndustryType = newIndustryType;
        ReplaceComponent(index, component);
    }

    public void RemoveIndustry() {
        RemoveComponent(GameComponentsLookup.Industry);
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

    static Entitas.IMatcher<GameEntity> _matcherIndustry;

    public static Entitas.IMatcher<GameEntity> Industry {
        get {
            if (_matcherIndustry == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Industry);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherIndustry = matcher;
            }

            return _matcherIndustry;
        }
    }
}

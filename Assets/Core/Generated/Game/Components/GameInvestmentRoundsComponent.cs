//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public InvestmentRoundsComponent investmentRounds { get { return (InvestmentRoundsComponent)GetComponent(GameComponentsLookup.InvestmentRounds); } }
    public bool hasInvestmentRounds { get { return HasComponent(GameComponentsLookup.InvestmentRounds); } }

    public void AddInvestmentRounds(InvestmentRound newInvestmentRound) {
        var index = GameComponentsLookup.InvestmentRounds;
        var component = (InvestmentRoundsComponent)CreateComponent(index, typeof(InvestmentRoundsComponent));
        component.InvestmentRound = newInvestmentRound;
        AddComponent(index, component);
    }

    public void ReplaceInvestmentRounds(InvestmentRound newInvestmentRound) {
        var index = GameComponentsLookup.InvestmentRounds;
        var component = (InvestmentRoundsComponent)CreateComponent(index, typeof(InvestmentRoundsComponent));
        component.InvestmentRound = newInvestmentRound;
        ReplaceComponent(index, component);
    }

    public void RemoveInvestmentRounds() {
        RemoveComponent(GameComponentsLookup.InvestmentRounds);
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

    static Entitas.IMatcher<GameEntity> _matcherInvestmentRounds;

    public static Entitas.IMatcher<GameEntity> InvestmentRounds {
        get {
            if (_matcherInvestmentRounds == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.InvestmentRounds);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInvestmentRounds = matcher;
            }

            return _matcherInvestmentRounds;
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CompletedGoalsComponent completedGoals { get { return (CompletedGoalsComponent)GetComponent(GameComponentsLookup.CompletedGoals); } }
    public bool hasCompletedGoals { get { return HasComponent(GameComponentsLookup.CompletedGoals); } }

    public void AddCompletedGoals(System.Collections.Generic.List<InvestorGoalType> newGoals) {
        var index = GameComponentsLookup.CompletedGoals;
        var component = (CompletedGoalsComponent)CreateComponent(index, typeof(CompletedGoalsComponent));
        component.Goals = newGoals;
        AddComponent(index, component);
    }

    public void ReplaceCompletedGoals(System.Collections.Generic.List<InvestorGoalType> newGoals) {
        var index = GameComponentsLookup.CompletedGoals;
        var component = (CompletedGoalsComponent)CreateComponent(index, typeof(CompletedGoalsComponent));
        component.Goals = newGoals;
        ReplaceComponent(index, component);
    }

    public void RemoveCompletedGoals() {
        RemoveComponent(GameComponentsLookup.CompletedGoals);
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

    static Entitas.IMatcher<GameEntity> _matcherCompletedGoals;

    public static Entitas.IMatcher<GameEntity> CompletedGoals {
        get {
            if (_matcherCompletedGoals == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CompletedGoals);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCompletedGoals = matcher;
            }

            return _matcherCompletedGoals;
        }
    }
}

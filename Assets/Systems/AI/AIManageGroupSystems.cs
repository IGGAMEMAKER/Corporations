using System.Collections.Generic;
using Entitas;

public partial class AIManageGroupSystems : OnQuarterChange
{
    public AIManageGroupSystems(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var companies = GetAIGroups();

        foreach (var c in companies)
            ManageGroup(c);
    }

    void ManageGroup(GameEntity c)
    {
        
    }

    GameEntity[] GetAIGroups()
    {
        return gameContext.GetEntities(
            GameMatcher
            .AllOf(GameMatcher.Company)
            .NoneOf(GameMatcher.Product)
        //.NoneOf(GameMatcher.ControlledByPlayer)
        );
    }
}

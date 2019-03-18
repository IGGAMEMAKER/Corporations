using Entitas;
using System.Collections.Generic;

public class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts)
    {

    }

    protected override void Execute(List<GameEntity> entities)
    {
        var AIProducts = gameContext.GetEntities(GameMatcher
            .AllOf(GameMatcher.Product)
            .NoneOf(GameMatcher.ControlledByPlayer)
            );

        foreach (var e in AIProducts)
        {
            
        }
    }
}

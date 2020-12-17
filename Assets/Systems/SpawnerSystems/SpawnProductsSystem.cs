using Assets.Core;
using System.Collections.Generic;

class SpawnProductsSystem : OnQuarterChange
{
    public SpawnProductsSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] niches = Markets.GetNiches(gameContext);

        foreach (var niche in niches)
            Markets.FillMarket(niche, gameContext);
    }
}

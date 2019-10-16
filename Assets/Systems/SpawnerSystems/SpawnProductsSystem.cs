using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class SpawnProductsSystem : OnQuarterChange
{
    public SpawnProductsSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] niches = NicheUtils.GetNiches(gameContext);

        foreach (var niche in niches)
            NicheUtils.FillMarket(niche, gameContext);
    }
}

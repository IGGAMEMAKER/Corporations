using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class SpawnProductsSystem : OnMonthChange
{
    public SpawnProductsSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] niches = contexts.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Niche));

        foreach (var e in niches)
        {
            if (e.nicheState.Phase == NicheLifecyclePhase.Death)
                continue;

            var nicheRating = NicheUtils.GetMarketRating(e);

            var potential = NicheUtils.GetMarketPotential(e);

            var playersOnMarket = NicheUtils.GetCompetitorsAmount(e.niche.NicheType, gameContext);

            var potentialRating = Mathf.Log10(potential) - 5;
            //                              1...5 = 25  *               1...4 = 10           
            var spawnChance = Mathf.Pow(nicheRating, 2) * Mathf.Pow(potentialRating, 1.7f) / (playersOnMarket + 1);

            if (spawnChance > Random.Range(0, 1000))
                CompanyUtils.AutoGenerateProductCompany(e.niche.NicheType, gameContext);
        }
    }
}

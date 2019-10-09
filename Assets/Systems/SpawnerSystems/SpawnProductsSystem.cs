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

        foreach (var niche in niches)
        {
            var phase = NicheUtils.GetMarketState(niche);
            if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
                continue;

            var nicheType = niche.niche.NicheType;

            var nicheRating = NicheUtils.GetMarketRating(niche);

            var potential = NicheUtils.GetMarketPotential(niche);

            var playersOnMarket = NicheUtils.GetCompetitorsAmount(nicheType, gameContext);

            var potentialRating = Mathf.Log10(potential) - 5;
            //                              1...5 = 25  *               1...4 = 10           
            var spawnChance = 2 * Mathf.Pow(nicheRating, 2) * Mathf.Pow(potentialRating, 1.7f) / (playersOnMarket + 1);

            if (spawnChance > Random.Range(0, 1000))
            {
                var product = CompanyUtils.AutoGenerateProductCompany(nicheType, gameContext);

                var startCapital = NicheUtils.GetStartCapital(niche) * Random.Range(50, 150) / 100;
                CompanyUtils.AddResources(product, new Assets.Classes.TeamResource(startCapital));
            }
        }
    }
}

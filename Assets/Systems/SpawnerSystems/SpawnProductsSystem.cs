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

                CompanyUtils.SetStartCapital(product, niche);

                if (CompanyUtils.IsInPlayerSphereOfInterest(product, gameContext))
                    NotificationUtils.AddPopup(gameContext, new PopupMessageCompanySpawn(product.company.Id));
            }
        }
    }
}

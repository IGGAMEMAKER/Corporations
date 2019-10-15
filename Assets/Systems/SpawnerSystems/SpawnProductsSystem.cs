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
            if (phase == NicheLifecyclePhase.Death)
                continue;

            var date = ScheduleUtils.GetCurrentDate(gameContext);
            var isReadyToBeOpen = niche.nicheLifecycle.OpenDate < date;
            if (!isReadyToBeOpen)
                continue;

            var nicheType = niche.niche.NicheType;
            var playersOnMarket = NicheUtils.GetCompetitorsAmount(nicheType, gameContext);

            if (phase == NicheLifecyclePhase.Innovation && playersOnMarket > 0)
                continue;

            var nicheRating = NicheUtils.GetMarketRating(niche);
            var potential = NicheUtils.GetMarketPotential(niche);

            var potentialRating = Mathf.Log10(potential) - 5;
            //                              1...5 = 25  *               1...4 = 10           
            var spawnChance = 2 * Mathf.Pow(nicheRating, 2) * Mathf.Pow(potentialRating, 1.7f) / (playersOnMarket + 1);

            if (playersOnMarket == 0 || phase == NicheLifecyclePhase.Idle)
            {
                spawnChance = 1200;

                if (Random.Range(0, 100) < 6)
                {
                    var player = CompanyUtils.GetPlayerCompany(gameContext);

                    if (player != null && CompanyUtils.IsInSphereOfInterest(player, nicheType))
                    {
                        NotificationUtils.AddPopup(gameContext, new PopupMessageMarketInspiration(nicheType));
                        continue;
                    }
                }
            }

            if (phase == NicheLifecyclePhase.Trending)
                spawnChance *= 5;

            if (spawnChance > Random.Range(0, 1000))
                SpawnCompany(niche);
        }
    }

    void SpawnCompany(GameEntity niche)
    {
        var product = CompanyUtils.AutoGenerateProductCompany(niche.niche.NicheType, gameContext);

        CompanyUtils.SetStartCapital(product, niche);

        if (CompanyUtils.IsInPlayerSphereOfInterest(product, gameContext))
            NotificationUtils.AddPopup(gameContext, new PopupMessageCompanySpawn(product.company.Id));
    }
}

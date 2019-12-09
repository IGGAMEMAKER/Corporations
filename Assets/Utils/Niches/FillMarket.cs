using UnityEngine;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static void FillMarket(GameEntity niche, GameContext gameContext)
        {
            var phase = GetMarketState(niche);
            if (phase == NicheLifecyclePhase.Death)
                return;

            var date = ScheduleUtils.GetCurrentDate(gameContext);
            var isReadyToBeOpen = niche.nicheLifecycle.OpenDate < date;
            if (!isReadyToBeOpen)
                return;

            var nicheType = niche.niche.NicheType;
            var playersOnMarket = GetCompetitorsAmount(nicheType, gameContext);

            if (phase == NicheLifecyclePhase.Innovation && playersOnMarket > 0)
                return;



            var nicheRating = GetMarketRating(niche);
            var potential = GetMarketPotential(niche);

            var potentialRating = Mathf.Log10(potential) - 5;
            //                              1...5 = 25  *               1...4 = 10           
            var spawnChance = 2 * Mathf.Pow(nicheRating, 2) * Mathf.Pow(potentialRating, 1.7f) / (playersOnMarket + 1);



            if (playersOnMarket == 0 || phase == NicheLifecyclePhase.Idle)
            {
                spawnChance = 1200;

                if (UnityEngine.Random.Range(0, 100) < 6)
                {
                    var player = Companies.GetPlayerCompany(gameContext);

                    if (player != null && Companies.IsInSphereOfInterest(player, nicheType))
                    {
                        NotificationUtils.AddPopup(gameContext, new PopupMessageMarketInspiration(nicheType));
                        return;
                    }
                }
            }

            if (phase == NicheLifecyclePhase.Trending)
                spawnChance *= 5;

            if (spawnChance > Random.Range(0, 1000))
                SpawnCompany(niche, gameContext);
        }

        public static void SpawnCompany(GameEntity niche, GameContext gameContext)
        {
            var product = Companies.AutoGenerateProductCompany(niche.niche.NicheType, gameContext);

            Companies.SetStartCapital(product, niche);

            var potentialLeader = NicheUtils.GetPotentialMarketLeader(gameContext, niche.niche.NicheType);
            var hasBiggestPotential = potentialLeader.company.Id == product.company.Id;

            if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && hasBiggestPotential)
                NotificationUtils.AddPopup(gameContext, new PopupMessageCompanySpawn(product.company.Id));
        }
    }
}

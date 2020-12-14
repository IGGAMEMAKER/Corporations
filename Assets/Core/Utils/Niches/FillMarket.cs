using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static void FillMarket(GameEntity niche, GameContext gameContext)
        {
            var phase = GetMarketState(niche);
            if (phase == MarketState.Death)
                return;

            var date = ScheduleUtils.GetCurrentDate(gameContext);
            var isReadyToBeOpen = niche.nicheLifecycle.OpenDate < date;
            if (!isReadyToBeOpen)
                return;

            var nicheType = niche.niche.NicheType;
            var playersOnMarket = 0; // GetCompetitorsAmount(nicheType, gameContext);

            // don't spawn companies on innovation phase if has companies already
            //if (phase == MarketState.Innovation && playersOnMarket > 0)
            //    return;

            bool isInPlayerSphereOfInfluence = Companies.IsInPlayerSphereOfInterest(niche.niche.NicheType, gameContext);

            TryToSpawnCompany(niche, gameContext, phase, isInPlayerSphereOfInfluence);
        }

        public static void TryToSpawnCompany(GameEntity niche, GameContext gameContext, MarketState phase, bool inPlayerSphereOfInfluence)
        {
            var segments = GetNichePositionings(niche.niche.NicheType, gameContext);

            var productsOnMarket = Markets.GetProductsOnMarket(niche, gameContext);
            bool willMakeSameProductWithPlayerFlagship = false;

            GameEntity playerFlagship = null;

            if (inPlayerSphereOfInfluence)
            {
                playerFlagship = Companies.GetPlayerFlagship(gameContext);

                if (niche.niche.NicheType == playerFlagship.product.Niche)
                    willMakeSameProductWithPlayerFlagship = true;
            }

            float intensity = 1f / (1 + segments.Count);

            if (inPlayerSphereOfInfluence)
                intensity = 1f;

            foreach (var s in segments)
            {
                var productsCount = productsOnMarket.Count(p => p.productPositioning.Positioning == s.ID);

                bool willCompeteDirectly = false;

                // increase chances if player has companeis in that segment
                if (inPlayerSphereOfInfluence)
                {
                    if (willMakeSameProductWithPlayerFlagship && Companies.IsDirectCompetitor(playerFlagship, niche, s.ID))
                    {
                        willCompeteDirectly = true;
                    }
                }

                var spawnChance = 0f;


                if (productsCount == 0)
                {
                    spawnChance = 20;
                }
                else if (productsCount == 1)
                {
                    spawnChance = 10;
                }
                else if (productsCount == 2)
                {
                    spawnChance = 5;
                }
                else
                {
                    spawnChance = 1;
                }

                if (willCompeteDirectly)
                    spawnChance *= 10;

                spawnChance *= intensity;

                // take into account competition strength too
                // Noone wants to make a browser, cause Google exists already

                if (Random.Range(0f, 100f) < spawnChance)
                {
                    var leaderFunds = Random.Range(50_000, 300_000);

                    var product = SpawnCompany(niche, gameContext, leaderFunds);
                    Marketing.ChangePositioning(product, gameContext, s.ID, true);

                    // NotificationUtils.SendNewCompetitorPopup(gameContext, niche, product);
                }
            }
        }

        public static bool InspirationToPlayer(NicheType nicheType, GameContext gameContext)
        {
            var inspirationChance = 6;

            if (inspirationChance > Random.Range(0, 100))
                return NotificationUtils.SendInspirationPopup(gameContext, nicheType);

            return false;
        }

        public static GameEntity SpawnCompany(GameEntity niche, GameContext gameContext, long leaderFunds)
        {
            var product = Companies.AutoGenerateProductCompany(niche.niche.NicheType, gameContext);

            Companies.SetResources(product, leaderFunds, "start capital for spawned company");

            return product;
        }
    }
}

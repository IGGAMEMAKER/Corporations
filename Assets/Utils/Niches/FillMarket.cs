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
            var playersOnMarket = GetCompetitorsAmount(nicheType, gameContext);

            // don't spawn companies on innovation phase if has companies already
            if (phase == MarketState.Innovation && playersOnMarket > 0)
                return;



            TryToSpawnCompany(niche, gameContext, phase, playersOnMarket);
        }

        public static bool IsNeedsMoreCompaniesOnMarket(GameEntity niche, GameContext gameContext, MarketState phase, int playersOnMarket)
        {
            // force spawning if there are no companies
            if (playersOnMarket == 0)
            {
                // offer spawning a company to player
                if (InspirationToPlayer(niche.niche.NicheType, gameContext))
                    return false;

                return true;
            }

            // competition is low
            return IsMarketCanAffordMoreCompanies(niche, gameContext);
        }

        public static bool IsAllReleasedCompaniesAreProfitable(GameEntity niche, GameContext gameContext)
        {
            var releasedProducts = GetProductsOnMarket(niche, gameContext).Where(p => p.isRelease);

            return releasedProducts.All(p => Economy.IsProfitable(gameContext, p.company.Id));
        }

        public static bool IsMarketCanAffordMoreCompanies(GameEntity niche, GameContext gameContext) => IsAllReleasedCompaniesAreProfitable(niche, gameContext);

        public static void TryToSpawnCompany(GameEntity niche, GameContext gameContext, MarketState phase, int playersOnMarket)
        {
            var leaderFunds = Random.Range(5000, 300000);

            bool leaderHasEnoughMoney = GetStartCapital(niche, gameContext) <= leaderFunds;

            if (IsNeedsMoreCompaniesOnMarket(niche, gameContext, phase, playersOnMarket) && leaderHasEnoughMoney)
            {
                var product = SpawnCompany(niche, gameContext, leaderFunds);

                NotificationUtils.SendNewCompetitorPopup(gameContext, niche, product);
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
            Companies.SetStartCapital(product, leaderFunds);

            return product;
        }
    }
}

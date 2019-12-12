using System.Linq;
using UnityEngine;

namespace Assets.Utils
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
                if (InspirationToPlayer(niche.niche.NicheType, gameContext))
                    return false;

                return true;
            }

            return false;
        }

        public static bool IsAllReleasedCompaniesAreProfiteering(GameEntity niche, GameContext gameContext)
        {
            var releasedProducts = GetProductsOnMarket(niche, gameContext).Where(p => p.isRelease);

            return releasedProducts.All(p => Economy.IsProfitable(gameContext, p.company.Id));
        }

        public static bool IsMarketCanAffordMoreCompanies(GameEntity niche, GameContext gameContext) => IsAllReleasedCompaniesAreProfiteering(niche, gameContext);

        public static void TryToSpawnCompany(GameEntity niche, GameContext gameContext, MarketState phase, int playersOnMarket)
        {
            var noCompanies = IsNeedsMoreCompaniesOnMarket(niche, gameContext, phase, playersOnMarket);
            var competitionIsLow = IsMarketCanAffordMoreCompanies(niche, gameContext);

            if (noCompanies || competitionIsLow)
                SpawnCompany(niche, gameContext);
        }

        public static bool InspirationToPlayer(NicheType nicheType, GameContext gameContext)
        {
            var inspirationChance = 6;
            if (inspirationChance > Random.Range(0, 100))
            {
                var player = Companies.GetPlayerCompany(gameContext);

                if (player != null && Companies.IsInSphereOfInterest(player, nicheType))
                {
                    NotificationUtils.AddPopup(gameContext, new PopupMessageMarketInspiration(nicheType));
                    return true;
                }
            }

            return false;
        }

        public static void SpawnCompany(GameEntity niche, GameContext gameContext)
        {
            var product = Companies.AutoGenerateProductCompany(niche.niche.NicheType, gameContext);
            Companies.SetStartCapital(product, niche, gameContext);


            var potentialLeader = GetPotentialMarketLeader(gameContext, niche.niche.NicheType);
            var hasBiggestPotential = potentialLeader.company.Id == product.company.Id;

            if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && hasBiggestPotential)
                NotificationUtils.AddPopup(gameContext, new PopupMessageCompanySpawn(product.company.Id));
        }
    }
}

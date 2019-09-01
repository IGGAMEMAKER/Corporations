using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static int GetMarketStateCostsModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation: return 1;
                case NicheLifecyclePhase.Trending: return 5;
                case NicheLifecyclePhase.MassUse: return 9;
                case NicheLifecyclePhase.Decay: return 7;

                default: return 0;
            }
        }

        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            var state = GetMarketState(niche);
            var costs = niche.nicheCosts;

            var multiplier = GetMarketStateCostsModifier(state);
            var component = new NicheCostsComponent
            {
                BasePrice = costs.BasePrice,
                AdCost = costs.AdCost * multiplier,
                ClientBatch = costs.ClientBatch * multiplier,
                IdeaCost = costs.IdeaCost * multiplier,
                MarketingCost = costs.MarketingCost * multiplier,
                TechCost = costs.TechCost * multiplier
            };

            return component;
        }

        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetNicheCosts(niche);
        }

        internal static long GetTeamMaintenanceCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var devCost = ProductUtils.GetBaseDevelopmentCost(niche);
            var marketingCost = GetBaseMarketingMaintenance(niche);

            var pp = devCost.programmingPoints;
            var mp = marketingCost.salesPoints;

            //Debug.Log($"pp: {pp}   sp: {mp}");
            var iteration = ProductUtils.GetBaseConceptTime(niche);

            var marketers = mp * Constants.SALARIES_MARKETER / Constants.DEVELOPMENT_PRODUCTION_MARKETER;
            var programmers = pp * Constants.SALARIES_PROGRAMMER * 30 / iteration / Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;

            return marketers + programmers;
        }

        internal static TeamResource GetBaseMarketingMaintenance(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            return new TeamResource(0, 0, costs.MarketingCost, 0, costs.AdCost);
        }

        public static long GetStartCapital(GameEntity niche)
        {
            var team = GetTeamMaintenanceCost(niche);
            var marketing = GetBaseMarketingMaintenance(niche);

            return team + marketing.money;
        }

        internal static TeamResource GetAggressiveMarketingMaintenance(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetAggressiveMarketingMaintenance(niche);
        }
        internal static TeamResource GetAggressiveMarketingMaintenance(GameEntity niche)
        {
            var baseCosts = GetBaseMarketingMaintenance(niche);

            return new TeamResource(0, 0, baseCosts.salesPoints * 3, 0, baseCosts.money * 20);
        }

        //#region branding
        //public static TeamResource GetPureBrandingCost(GameEntity niche)
        //{
        //    var costs = NicheUtils.GetNicheCosts(niche);

        //    return new TeamResource(0, 0, costs.MarketingCost * 4, 0, costs.AdCost * 3);
        //}

        //public static TeamResource GetPureBrandingCost(GameContext gameContext, NicheType nicheType)
        //{
        //    var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

        //    return GetPureBrandingCost(niche);
        //}

        //public static TeamResource GetPureBrandingCost(GameContext gameContext, GameEntity company)
        //{
        //    return GetPureBrandingCost(gameContext, company.product.Niche);
        //}

        ////public static TeamResource GetPureBrandingCostPerMonth(GameEntity niche)
        ////{

        ////}
        //#endregion

        //#region targeting
        //public static TeamResource GetPureTargetingCost(GameEntity niche)
        //{
        //    var costs = NicheUtils.GetNicheCosts(niche);

        //    return new TeamResource(0, 0, costs.MarketingCost * 1, 0, costs.AdCost * 1);
        //}

        //public static TeamResource GetPureTargetingCost(GameContext gameContext, NicheType nicheType)
        //{
        //    var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

        //    return GetPureTargetingCost(niche);
        //}

        //public static TeamResource GetPureTargetingCost(GameContext gameContext, GameEntity company)
        //{
        //    return GetPureTargetingCost(gameContext, company.product.Niche);
        //}
        //#endregion
    }
}

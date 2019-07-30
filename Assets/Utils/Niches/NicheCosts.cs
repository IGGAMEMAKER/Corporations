using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static NicheCostsComponent GetNicheCosts(GameEntity niche)
        {
            return niche.nicheCosts;
        }

        public static NicheCostsComponent GetNicheCosts(GameContext context, NicheType nicheType)
        {
            var niche = GetNicheEntity(context, nicheType);

            return GetNicheCosts(niche);
        }

        #region branding
        public static TeamResource GetPureBrandingCost(GameEntity niche)
        {
            var costs = NicheUtils.GetNicheCosts(niche);

            return new TeamResource(0, 0, costs.MarketingCost * 4, 0, costs.AdCost * 3);
        }

        public static TeamResource GetPureBrandingCost(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            return GetPureBrandingCost(niche);
        }

        public static TeamResource GetPureBrandingCost(GameContext gameContext, GameEntity company)
        {
            return GetPureBrandingCost(gameContext, company.product.Niche);
        }

        //public static TeamResource GetPureBrandingCostPerMonth(GameEntity niche)
        //{

        //}
        #endregion

        #region targeting
        public static TeamResource GetPureTargetingCost(GameEntity niche)
        {
            var costs = NicheUtils.GetNicheCosts(niche);

            return new TeamResource(0, 0, costs.MarketingCost * 1, 0, costs.AdCost * 1);
        }

        public static TeamResource GetPureTargetingCost(GameContext gameContext, NicheType nicheType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);

            return GetPureTargetingCost(niche);
        }

        public static TeamResource GetPureTargetingCost(GameContext gameContext, GameEntity company)
        {
            return GetPureTargetingCost(gameContext, company.product.Niche);
        }
        #endregion

        internal static long GetTeamMaintenanceCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var devCost = ProductUtils.GetDevelopmentCost(niche);
            var marketingCost = GetBaseMarketingMaintenance(niche);

            var pp = devCost.programmingPoints;
            var mp = marketingCost.salesPoints;

            //Debug.Log($"pp: {pp}   sp: {mp}");

            var marketers = mp * Constants.SALARIES_MARKETER / Constants.DEVELOPMENT_PRODUCTION_MARKETER;
            var programmers = pp * Constants.SALARIES_PROGRAMMER / Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;

            return marketers + programmers;
        }

        internal static TeamResource GetBaseMarketingMaintenance(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var brandingDuration = (long)MarketingUtils.GetBrandingCampaignCooldownDuration();
            var targetingDuration = (long)MarketingUtils.GetTargetingDuration();

            var branding = GetPureBrandingCost(niche);
            var targeting = GetPureTargetingCost(niche);

            // TODO period???
            long month = 30;

            var targetingPerMonth = targeting * month / targetingDuration;
            var brandingPerMonth = branding * month / brandingDuration;

            var marketingCost = targetingPerMonth + brandingPerMonth;

            return marketingCost;
        }
    }
}

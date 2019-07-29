using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static TeamResource GetPureTargetingCost(GameEntity niche)
        {
            var costs = niche.nicheCosts;

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


        public static TeamResource GetTargetingCost(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);
            var financing = GetMarketingFinancingPriceModifier(c.finance.marketingFinancing);

            return GetPureTargetingCost(gameContext, c) * financing;
        }

        public static void EnableTargeting(GameEntity company)
        {
            company.isTargeting = true;
        }

        public static long GetCompanyBrandModifierMultipliedByHundred(GameEntity e)
        {
            return 100 + e.branding.BrandPower * 100 / 2;
        }

        public static long GetCompanyReachModifierMultipliedByHundred(GameEntity e)
        {
            var financing = GetMarketingFinancingAudienceReachModifier(e.finance.marketingFinancing);

            var brand = GetCompanyBrandModifierMultipliedByHundred(e);

            return financing * brand;
        }

        public static long GetTargetingEffeciency(GameContext gameContext, GameEntity e)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, e.product.Niche);

            long baseForNiche = GetCompanyClientBatch(gameContext, e);

            long reachModifier = GetCompanyReachModifierMultipliedByHundred(e);


            return baseForNiche * reachModifier / 100;
        }
    }
}

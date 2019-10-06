using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClients(GameEntity company)
        {
            return company.marketing.clients;
        }

        public static void AddClients(GameEntity company, long clients)
        {
            var marketing = company.marketing;

            company.ReplaceMarketing(marketing.clients + clients);
        }

        public static long GetCurrentClientFlow(GameContext gameContext, NicheType nicheType)
        {
            //var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
            //var phase = NicheUtils.GetMarketState(niche);

            //var baseGrowthModifier = niche.nicheLifecycle.Growth[phase];

            var costs = NicheUtils.GetNicheCosts(gameContext, nicheType);

            return costs.ClientBatch; // * baseGrowthModifier;
        }

        public static float GetMarketShareBasedBrandDecay(GameEntity product, GameContext gameContext)
        {
            var marketShare = (float)CompanyUtils.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext);
            var brand = product.branding.BrandPower;

            var change = (marketShare - brand) / 10;

            return change;
        }

        public static BonusContainer GetMonthlyBrandPowerChange(GameEntity product, GameContext gameContext)
        {
            bool isPayingForMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPolishedApp);
            bool isPayingForAggressiveMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentCrossplatform);

            //Debug.Log("RecalculateBrandPowers: " + product.company.Name + " isPayingForMarketing=" + isPayingForMarketing);


            var conceptStatus = ProductUtils.GetConceptStatus(product, gameContext);
            var isOutOfMarket = conceptStatus == ConceptStatus.Outdated;
            var isInnovator = conceptStatus == ConceptStatus.Leader;

            var decay = GetMarketShareBasedBrandDecay(product, gameContext);

            var percent = 4;
            var baseDecay = -product.branding.BrandPower * percent / 100;

            var BrandingChangeBonus = new BonusContainer("Brand power change")
                //.Append("Base", -1)
                .AppendAndHideIfZero(percent + "% Decay", (int)baseDecay)
                //.Append("Due to Market share", (int)decay)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -1 : 0)
                .AppendAndHideIfZero("Is Innovator", isInnovator ? 2 : 0)
                .AppendAndHideIfZero("Is Paying For Marketing", isPayingForMarketing ? 1 : 0)
                .AppendAndHideIfZero("Aggressive Marketing", isPayingForAggressiveMarketing ? 3 : 0);
                //.AppendAndHideIfZero("Is Innovator + Aggressive Marketing", isInnovator && isPayingForAggressiveMarketing ? 6 : 0);

            return BrandingChangeBonus;
        }

        public static long GetAudienceGrowth(GameEntity product, GameContext gameContext)
        {
            var flow = GetCurrentClientFlow(gameContext, product.product.Niche);
            var multiplier = GetAudienceGrowthMultiplier(product, gameContext);

            return (long)(multiplier * flow);
        }

        public static float GetAudienceGrowthMultiplier(GameEntity product, GameContext gameContext)
        {
            var SEO = (product.branding.BrandPower + 100) / 100;

            var marketing = 0;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPrototype))
                marketing = 1;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentPolishedApp))
                marketing = 3;
            if (TeamUtils.IsUpgradePicked(product, TeamUpgrade.DevelopmentCrossplatform))
                marketing = 9;

            var conceptModifier = ProductUtils.GetDifferenceBetweenMarketDemandAndAppConcept(product, gameContext);

            // SEO: 0...2
            // marketing: 0...12
            return (SEO + marketing * 3) / (conceptModifier + 1); // + rand * 50;
        }
    }
}

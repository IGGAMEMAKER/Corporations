using Assets.Classes;
using System.Text;
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
            bool isPayingForMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.MarketingBase);
            bool isPayingForAggressiveMarketing = TeamUtils.IsUpgradePicked(product, TeamUpgrade.MarketingAggressive);
            
            //Debug.Log("RecalculateBrandPowers: " + product.company.Name + " isPayingForMarketing=" + isPayingForMarketing);

            var isOutOfMarket = ProductUtils.IsOutOfMarket(product, gameContext);
            var isInnovator = product.isTechnologyLeader;

            var decay = GetMarketShareBasedBrandDecay(product, gameContext);
            var paymentModifier = isPayingForMarketing;

            var baseDecay = -product.branding.BrandPower * 5 / 100;

            var BrandingChangeBonus = new BonusContainer("Brand power change")
                .Append("Base", -1)
                .AppendAndHideIfZero("5% Decay", (int)baseDecay)
                //.Append("Due to Market share", (int)decay)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -1 : 0)
                .AppendAndHideIfZero("Is Paying For Marketing", paymentModifier ? 1 : 0)
                .AppendAndHideIfZero("Is Innovator", isInnovator ? 2 : 0)
                .AppendAndHideIfZero("Aggressive Marketing", isPayingForAggressiveMarketing ? 3 : 0)
                .AppendAndHideIfZero("Is Innovator + Aggressive Marketing", isInnovator && isPayingForAggressiveMarketing ? 6 : 0);

            return BrandingChangeBonus;
        }


        public static TeamResource GetReleaseCost()
        {
            return new TeamResource(0, 0, 50, 0, 1000);
        }

        public static int GetReleaseBrandPowerGain()
        {
            return 20;
        }

        public static void ReleaseApp(GameEntity product)
        {
            var need = GetReleaseCost();

            bool enoughResources = CompanyUtils.IsEnoughResources(product, need);

            if (!product.isRelease && enoughResources)
            {
                product.isRelease = true;
                AddBrandPower(product, GetReleaseBrandPowerGain());

                CompanyUtils.SpendResources(product, need);
            }
        }
    }
}

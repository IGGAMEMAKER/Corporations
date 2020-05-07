using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Products
    {
        public static bool IsUpgradeEnabled(GameEntity product, ProductUpgrade productUpgrade)
        {
            var u = product.productUpgrades.upgrades;

            return u.ContainsKey(productUpgrade) && u[productUpgrade];
        }

        public static bool CanEnable(GameEntity company, GameContext gameContext, ProductUpgrade productUpgrade)
        {
            var marketState = Markets.GetMarketState(gameContext, company.product.Niche);

            var goal = company.companyGoal.InvestorGoal;


            bool preReleaseOrLater = goal >= InvestorGoal.Release;
            bool afterRelease = company.isRelease;

            bool QATeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateQATeam);
            bool CoreTeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateManagementTeam);
            bool SupportTeamCreated = Products.IsUpgradeEnabled(company, ProductUpgrade.CreateSupportTeam);

            switch (productUpgrade)
            {
                case ProductUpgrade.AutorecruitWorkers: return false;

                case ProductUpgrade.PlatformDesktop: return true;
                case ProductUpgrade.PlatformMobileAndroid:
                case ProductUpgrade.PlatformMobileIOS:
                case ProductUpgrade.PlatformWeb:
                    return true;

                case ProductUpgrade.Support:
                case ProductUpgrade.Support2:
                case ProductUpgrade.Support3:
                    return preReleaseOrLater && SupportTeamCreated;

                case ProductUpgrade.QA:
                case ProductUpgrade.QA2:
                case ProductUpgrade.QA3:
                    return preReleaseOrLater && QATeamCreated;

                    // teams
                case ProductUpgrade.CreateManagementTeam:
                    return preReleaseOrLater && !CoreTeamCreated;

                case ProductUpgrade.CreateSupportTeam:
                    return preReleaseOrLater && CoreTeamCreated && !SupportTeamCreated;

                case ProductUpgrade.CreateQATeam:
                    return preReleaseOrLater && CoreTeamCreated && !QATeamCreated;

                    // marketing
                case ProductUpgrade.TargetingCampaign:
                    return afterRelease && marketState >= MarketState.Innovation;

                case ProductUpgrade.TargetingCampaign2:
                    return afterRelease && marketState >= MarketState.Trending;

                case ProductUpgrade.TargetingCampaign3:
                    return afterRelease && marketState >= MarketState.MassGrowth;

                case ProductUpgrade.BrandCampaign:
                    return afterRelease && marketState >= MarketState.Innovation;

                case ProductUpgrade.BrandCampaign2:
                    return afterRelease && marketState >= MarketState.Trending;

                case ProductUpgrade.BrandCampaign3:
                    return afterRelease && marketState >= MarketState.MassGrowth;

                default:
                    return true;
            }
        }

        public static void SetUpgrade(GameEntity product, ProductUpgrade upgrade, GameContext gameContext, bool state)
        {
            if (state && !CanEnable(product, gameContext, upgrade))
                return;

            product.productUpgrades.upgrades[upgrade] = state;


            if (state)
            {
                var qa = new List<ProductUpgrade>() { ProductUpgrade.QA, ProductUpgrade.QA2, ProductUpgrade.QA3 };
                var support = new List<ProductUpgrade>() { ProductUpgrade.Support, ProductUpgrade.Support2, ProductUpgrade.Support3 };

                if (qa.Contains(upgrade))
                    qa.ForEach(u => product.productUpgrades.upgrades[u] = u == upgrade);

                if (support.Contains(upgrade))
                    support.ForEach(u => product.productUpgrades.upgrades[u] = u == upgrade);
            }

            ScaleTeam(product, gameContext);
        }
    }
}

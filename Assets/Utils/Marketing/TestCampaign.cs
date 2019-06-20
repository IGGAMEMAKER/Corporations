using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartTestCampaign(GameContext gameContext, GameEntity company)
        {
            if (CompanyUtils.HasCooldown(company, CooldownType.TestCampaign))
                return;

            var cost = GetTestCampaignCost(gameContext, company);

            if (!CompanyUtils.IsEnoughResources(company, cost))
                return;

            CompanyUtils.AddCooldown(gameContext, company, CooldownType.TestCampaign, 15);

            AddClients(company, UserType.Core, GetTestCampaignClientGain(gameContext, company));

            int feedback = UnityEngine.Random.Range(25, 75);
            CompanyUtils.AddResources(company, new TeamResource().AddIdeas(feedback));

            CompanyUtils.SpendResources(company, cost);
        }

        public static long GetTestCampaignClientGain(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            return costs.ClientBatch / 4;
        }

        public static TeamResource GetTestCampaignCost(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            var marketingCost = costs.MarketingCost;

            return new TeamResource(0, 0, marketingCost, 0, 0);
        }
    }
}

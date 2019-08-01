using Assets.Classes;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartTestCampaign(GameContext gameContext, GameEntity company)
        {
            if (CooldownUtils.HasCooldown(company, CooldownType.TestCampaign))
                return;

            var cost = GetTestCampaignCost(gameContext, company);

            if (!CompanyUtils.IsEnoughResources(company, cost))
                return;

            var testClients = GetTestCampaignClientGain(gameContext, company);

            AddClients(company, testClients);
            GetFeedbackFromTestCampaign(company);

            var duration = GetTestCampaignDuration(gameContext, company);

            CooldownUtils.AddCooldownAndSpendResources(gameContext, company, CooldownType.TestCampaign, duration, cost);
        }

        public static int GetTestCampaignDuration(GameContext gameContext, GameEntity company)
        {
            return Constants.COOLDOWN_TEST_CAMPAIGN;
        }

        public static long GetTestCampaignClientGain(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, company.product.Niche);

            return costs.ClientBatch / 4;
        }

        public static void GetFeedbackFromTestCampaign(GameEntity company)
        {
            int feedback = UnityEngine.Random.Range(25, 75);

            CompanyUtils.AddResources(company, new TeamResource().AddIdeas(feedback));
        }

        public static TeamResource GetTestCampaignCost(GameContext gameContext, GameEntity company)
        {
            var costs = NicheUtils.GetNicheCosts(gameContext, company.product.Niche);

            return new TeamResource(0, 0, costs.MarketingCost, 0, 0);
        }
    }
}

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetBaseIterationTime(GameContext gameContext, GameEntity company) => GetBaseIterationTime(Markets.Get(gameContext, company));
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheBaseProfile.Profile.NicheSpeed);
        public static int GetBaseIterationTime(NicheSpeed nicheChangeSpeed)
        {
            return 4;
        }


        public static int GetTotalDevelopmentEffeciency(GameContext gameContext, GameEntity product)
        {
            var teamSizeModifier = Products.GetTeamEffeciency(gameContext, product);

            // team lead
            // 0...50
            var managerBonus = Teams.GetTeamLeadDevelopmentTimeDiscount(gameContext, product);

            var speed = teamSizeModifier * (100 + managerBonus) / 100;

            return speed;
        }


        public static int GetTeamEffeciency(GameContext gameContext, GameEntity product)
        {
            return (int) (100 * GetTeamSizeMultiplier(gameContext, product));
        }

        public static float GetTeamSizeMultiplier(GameContext gameContext, GameEntity company)
        {
            // +1 - CEO
            var have     = Teams.GetTeamSize(company) + 1f;
            var required = Products.GetNecessaryAmountOfWorkers(company, gameContext) + 1f;

            if (have >= required)
                have = required;

            return have / required;
        }

        public static int GetIterationTimeCost(GameEntity product, GameContext gameContext)
        {
            var baseCost = Products.GetBaseIterationTime(gameContext, product);

            bool willInnovate = Products.IsWillInnovate(product, gameContext);

            var innovationPenalty = willInnovate ? 250 : 100;

            var isReleasedPenalty = product.isRelease ? 2 : 1;

            return baseCost * innovationPenalty * isReleasedPenalty;
        }

        public static int GetTimeToMarketFromScratch(GameEntity niche)
        {
            var demand = GetMarketDemand(niche);
            var iterationTime = GetBaseIterationTime(niche);

            return demand * iterationTime / 2 / 30;
        }
    }
}

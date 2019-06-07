namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static int GetCompanyRating(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            if (c.hasProduct)
                return GetProductCompanyRating(gameContext, c);

            return UnityEngine.Random.Range(1, 6);
        }

        static int GetProductCompanyRating(GameContext gameContext, GameEntity product)
        {
            var value = 3;

            if (product.isTechnologyLeader)
                value++;
            else
            {
                var diff = MarketingUtils.GetMarketDiff(gameContext, product);

                if (diff > 1)
                    value--;
            }

            return value;
        }
    }
}

namespace Assets.Core
{
    public static partial class Economy
    {
        public static int GetCompanyRating(GameContext gameContext, int companyId)
        {
            var c = Companies.Get(gameContext, companyId);

            if (c.hasProduct)
                return GetProductCompanyRating(gameContext, c);

            return GetGroupCompanyRating(gameContext, c);
        }

        static int GetGroupCompanyRating(GameContext gameContext, GameEntity company)
        {
            var cost = CostOf(company, gameContext);
            var rank = System.Math.Log10(cost) / 1.5d;

            return 1 + UnityEngine.Mathf.Clamp((int)rank, 0, 4);

            //return UnityEngine.Random.Range(1, 6);
        }

        static int GetProductCompanyRating(GameContext gameContext, GameEntity product)
        {
            var value = 3;

            //if (product.isTechnologyLeader)
            //    value++;
            //else
            //{
            //    var diff = MarketingUtils.GetMarketDiff(gameContext, product);

            //    if (diff > 1)
            //        value--;
            //}

            return value;
        }
    }
}

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetTargetingCost(GameEntity product, GameContext gameContext)
        {
            var clientCost = Markets.GetClientAcquisitionCost(product.product.Niche, gameContext);
            var flow = GetClientFlow(gameContext, product.product.Niche);

            var cost = clientCost * flow;

            var discount = GetCorpCultureMarketingDiscount(product, gameContext);

            var result = cost * discount / 100;

            return (long)result;
        }

        public static int GetCorpCultureMarketingDiscount(GameEntity product, GameContext gameContext)
        {
            var culture = Companies.GetActualCorporateCulture(product, gameContext);
            var creation = culture[CorporatePolicy.BuyOrCreate];

            // up to 40%
            var discount = 100 - (creation - 1) * 5;

            return discount;
        }
    }
}

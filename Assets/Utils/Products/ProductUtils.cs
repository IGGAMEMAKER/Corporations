using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void SetPrice(GameEntity e, Pricing pricing)
        {
            e.ReplaceFinance(pricing, e.finance.marketingFinancing, e.finance.salaries, e.finance.basePrice);
        }

        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }
    }
}

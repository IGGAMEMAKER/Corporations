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


        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static bool HasSegmentCooldown(GameEntity product, UserType userType)
        {
            var cooldown = new CooldownImproveSegment(userType);

            return CooldownUtils.HasCooldown(product, cooldown);
        }
    }
}

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }

        public static bool IsHasAvailableProductImprovements(GameEntity product)
        {
            var level = GetProductLevel(product);
            var improvements = product.productImprovements.Count;

            return level > improvements;
        }

        public static void SetFinancing(GameEntity product, Financing financing, int level)
        {
            product.financing.Financing[financing] = level;
        }
    }
}

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }

        public const int GetMaxFinancing = 2;

        public static int GetExpertiseGain(GameEntity c)
        {
            return 2;
        }

        //public static void SetMarketingFinancing(GameEntity product, int level)
        //{
        //    SetFinancing(product, Financing.Marketing, level);
        //}

        //public static void SetFinancing(GameEntity product, Financing financing, int level)
        //{
        //    product.financing.Financing[financing] = level;
        //}
    }
}

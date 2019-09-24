using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        internal static int GetProductLevel(GameEntity c)
        {
            return c.product.Concept;
        }
    }
}

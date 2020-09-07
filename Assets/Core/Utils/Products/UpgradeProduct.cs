using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        // Concept upgrade
        public static void ForceUpgrade(GameEntity product, GameContext gameContext, int upgrade)
        {
            product.ReplaceProduct(product.product.Niche, Products.GetProductLevel(product) + upgrade);

            Markets.UpdateMarketRequirements(product, gameContext);
        }
    }
}

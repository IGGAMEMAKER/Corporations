using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Products
    {
        public static bool IsUpgradeEnabled(GameEntity product, ProductUpgrade productUpgrade)
        {
            var u = product.productUpgrades.upgrades;

            return u.ContainsKey(productUpgrade) && u[productUpgrade];
        }
    }
}

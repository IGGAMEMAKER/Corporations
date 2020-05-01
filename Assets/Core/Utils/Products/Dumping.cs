using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Products
    {
        public static void ToggleDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = !product.isDumping;
        }

        public static void StartDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = true;
        }

        public static void StopDumping(GameContext gameContext, GameEntity product)
        {
            product.isDumping = false;
        }

        // --------------------------------

        public static bool IsUpgradeEnabled(GameEntity product, ProductUpgrade productUpgrade)
        {
            var u = product.productUpgrades.upgrades;

            return u.ContainsKey(productUpgrade) && u[productUpgrade];
        }

        public static void SetUpgrade(GameEntity product, ProductUpgrade upgrade, GameContext gameContext, bool state)
        {
            product.productUpgrades.upgrades[upgrade] = state;

            var qa = new List<ProductUpgrade>() { ProductUpgrade.QA, ProductUpgrade.QA2, ProductUpgrade.QA3 };
            var support = new List<ProductUpgrade>() { ProductUpgrade.Support, ProductUpgrade.Support2, ProductUpgrade.Support3 };

            if (state)
            {
                if (qa.Contains(upgrade))
                    qa.ForEach(u => product.productUpgrades.upgrades[u] = u == upgrade);

                if (support.Contains(upgrade))
                    support.ForEach(u => product.productUpgrades.upgrades[u] = u == upgrade);
            }

            ScaleTeam(product, gameContext);
        }
    }
}

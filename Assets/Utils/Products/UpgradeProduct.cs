using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetFreeImprovements(GameEntity product)
        {
            return product.expertise.ExpertiseLevel;
        }

        public static bool HasFreeImprovements(GameEntity product)
        {
            return GetFreeImprovements(product) > 0;
        }

        // TODO move to separate file/delete
        public static void UpgradeFeatures(ProductFeature improvement, GameEntity product, GameContext gameContext)
        {
            var task = new CompanyTaskUpgradeFeature(product.company.Id, improvement);

            if (CanUpgradeFeature(improvement, product, gameContext, task))
            {
                product.expertise.ExpertiseLevel--;
                Cooldowns.AddCooldown(gameContext, task, 8);
            }
        }

        public static bool CanUpgradeFeature(ProductFeature improvement, GameEntity product, GameContext gameContext, CompanyTask task)
        {
            return HasFreeImprovements(product) && !Cooldowns.HasCooldown(gameContext, task);
        }
    }
}

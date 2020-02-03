using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    partial class Cooldowns
    {
        //
        // specific cooldowns
        //

        // concept upgrade cooldown
        public static void AddConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            int duration = Products.GetConceptUpgradeTime(gameContext, product);
            
            //AddCooldown(gameContext, new CooldownImproveConcept(product.company.Id), duration);
        }

        public static bool HasConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            return true;
            //return HasCooldown(gameContext, new CooldownImproveConcept(product.company.Id));
        }

        // culture upgrade cooldown
        public static void AddCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity product, int duration)
        {
            //AddCooldown(gameContext, new CooldownUpgradeCorporateCulture(product.company.Id), duration);
        }

        public static bool HasCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company)
        {
            return true;
            //return HasCooldown(gameContext, new CooldownUpgradeCorporateCulture(company.company.Id));
        }
    }
}

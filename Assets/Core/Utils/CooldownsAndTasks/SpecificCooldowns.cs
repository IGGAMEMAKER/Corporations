using UnityEngine;

namespace Assets.Core
{
    partial class Cooldowns
    {
        // concept upgrade cooldown
        //public static void AddConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        //{
        //    AddCooldown(gameContext, new CompanyTaskUpgradeConcept(product.company.Id), Products.GetConceptUpgradeTime(gameContext, product));
        //}

        //public static bool HasConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        //{
        //    return HasCooldown(gameContext, new CompanyTaskUpgradeConcept(product.company.Id));
        //}


        // culture upgrade cooldown
        public static void AddCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company, int duration)
        {
            AddCooldown(gameContext, new CompanyTaskUpgradeCulture(company.company.Id), duration);
        }

        public static bool HasCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company)
        {
            return HasCooldown(gameContext, new CompanyTaskUpgradeCulture(company.company.Id));
        }

        public static TimedActionComponent GetCorporateCultureCooldown(GameEntity company, GameContext gameContext)
        {
            return Cooldowns.GetTask(gameContext, new CompanyTaskUpgradeCulture(company.company.Id));
        }
    }
}

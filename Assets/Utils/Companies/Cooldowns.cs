using Entitas;
using System.Collections.Generic;

namespace Assets.Core
{
    partial class Cooldowns
    {
        // new cooldwon system
        private static GameEntity GetCooldownContainer(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.CooldownContainer)[0];
        }

        public static Dictionary<string, Cooldown> GetCooldowns(GameContext gameContext)
        {
            var container = GetCooldownContainer(gameContext);

            return container.cooldownContainer.Cooldowns;
        }


        public static bool HasCooldown(GameContext gameContext, Cooldown cooldown) => HasCooldown(gameContext, cooldown.GetKey());
        public static bool HasCooldown(GameContext gameContext, string cooldownName)
        {
            return GetCooldowns(gameContext).ContainsKey(cooldownName);
        }

        public static void AddNewCooldown(GameContext gameContext, Cooldown cooldown, int duration) => AddNewCooldown(gameContext, cooldown.GetKey(), cooldown, duration);
        public static void AddNewCooldown(GameContext gameContext, string cooldownName, Cooldown cooldown, int duration)
        {
            var c = GetCooldowns(gameContext);

            cooldown.EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration;

            c[cooldownName] = cooldown;
        }

        public static bool TryGetCooldown(GameContext gameContext, Cooldown req, out Cooldown cooldown) => TryGetCooldown(gameContext, req.GetKey(), out cooldown);
        public static bool TryGetCooldown(GameContext gameContext, string cooldownName, out Cooldown cooldown)
        {
            return GetCooldowns(gameContext).TryGetValue(cooldownName, out cooldown);
        }

        //
        // specific cooldowns
        //

        // concept upgrade cooldown
        public static void AddConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            int duration = Products.GetConceptUpgradeTime(gameContext, product);
            
            AddNewCooldown(gameContext, new CooldownImproveConcept(product.company.Id), duration);
        }

        public static bool HasConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            return HasCooldown(gameContext, new CooldownImproveConcept(product.company.Id));
        }

        // culture upgrade cooldown
        public static void AddCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity product, int duration)
        {
            AddNewCooldown(gameContext, new CooldownUpgradeCorporateCulture(product.company.Id), duration);
        }

        public static bool HasCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company)
        {
            return HasCooldown(gameContext, new CooldownUpgradeCorporateCulture(company.company.Id));
        }
    }
}

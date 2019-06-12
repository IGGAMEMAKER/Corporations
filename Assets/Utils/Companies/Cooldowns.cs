using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            AddCooldown(gameContext, company, new Cooldown { CooldownType = cooldownType }, duration);
        }

        public static void AddCooldown(GameContext gameContext, GameEntity company, Cooldown cooldown, int duration)
        {
            if (HasCooldown(company, cooldown))
                return;

            cooldown.EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration;

            var cooldowns = company.cooldowns.Cooldowns;
            cooldowns.Add(cooldown);

            company.ReplaceCooldowns(cooldowns);
        }


        public static bool HasCooldown(GameEntity gameEntity, CooldownType cooldownType)
        {
            return gameEntity.cooldowns.Cooldowns.Find(cd => cd.Compare(cooldownType)) != null;
        }

        public static bool HasCooldown(GameEntity gameEntity, Cooldown cooldown)
        {
            return gameEntity.cooldowns.Cooldowns.Find(cd => cd.Compare(cooldown)) != null;
        }
    }
}

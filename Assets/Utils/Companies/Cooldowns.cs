using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            var cooldowns = company.cooldowns.Cooldowns;

            //Debug.Log("Add Cooldown " + cooldownType.ToString() + ": " + company.company.Name + " " + company.company.Id);

            AddCooldown(gameContext, company, new Cooldown
            {
                CooldownType = cooldownType
            }, duration);
        }

        public static void AddCooldown(GameContext gameContext, GameEntity company, Cooldown cooldown, int duration)
        {
            var cooldowns = company.cooldowns.Cooldowns;

            if (HasCooldown(company, cooldown))
                return;

            cooldown.EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration;
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

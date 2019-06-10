using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            var cooldowns = company.cooldowns.Cooldowns;
            Debug.Log("Add Cooldown " + cooldownType.ToString() + ": " + company.company.Name + " " + company.company.Id);
            Debug.Log("Cooldowns: " + cooldowns.Count);

            if (cooldowns.Find(cd => cd.CooldownType == cooldownType) != null)
                return;

            AddCooldown(gameContext, company, new Cooldown
            {
                CooldownType = cooldownType
            });
        }

        public static void AddCooldown(GameContext gameContext, GameEntity company, Cooldown cooldown, int duration = 0)
        {
            var cooldowns = company.cooldowns.Cooldowns;

            cooldown.EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration;
            cooldowns.Add(cooldown);

            company.ReplaceCooldowns(cooldowns);
        }

        public static bool HasCooldown(GameEntity gameEntity, CooldownType cooldownType)
        {
            return gameEntity.cooldowns.Cooldowns.Find(cd => cd.CooldownType == cooldownType) != null;
        }
    }
}

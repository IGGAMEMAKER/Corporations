namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            var c = company.cooldowns.Cooldowns;

            if (c.ContainsKey(cooldownType))
                return;

            c[cooldownType] = new Cooldown { EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration };

            company.ReplaceCooldowns(c);
        }

        public static bool HasCooldown(GameEntity gameEntity, CooldownType cooldownType)
        {
            return gameEntity.cooldowns.Cooldowns.ContainsKey(cooldownType);
        }
    }
}

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            var cooldowns = company.cooldowns.Cooldowns;

            if (cooldowns.Find(cd => cd.CooldownType == cooldownType) != null)
                return;

            cooldowns.Add(new Cooldown { EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration });

            company.ReplaceCooldowns(cooldowns);
        }

        public static void AddCooldown(GameContext gameContext, GameEntity company, Cooldown cooldown, int duration)
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

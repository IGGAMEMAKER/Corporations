using Entitas;

namespace Assets.Core
{
    partial class Cooldowns
    {
        public static GameEntity[] GetCooldowns(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.TimedAction, GameMatcher.Cooldown));
        }

        public static bool HasCooldown(GameContext gameContext, CompanyTask task)
        {
            return IsHasTask(gameContext, task);
        }

        public static void AddCooldown(GameContext gameContext, CompanyTask task, int duration)
        {
            var t = AddTimedAction(gameContext, task, duration);

            if (t != null)
            {
                ProcessTask(t.timedAction, gameContext);
                t.isCooldown = true;
            }
        }

        //public static bool TryGetCooldown(GameContext gameContext, Cooldown req, out Cooldown cooldown) => TryGetCooldown(gameContext, req.GetKey(), out cooldown);
        //public static bool TryGetCooldown(GameContext gameContext, string cooldownName, out Cooldown cooldown)
        //{
        //    return GetCooldowns(gameContext).TryGetValue(cooldownName, out cooldown);
        //}
    }
}

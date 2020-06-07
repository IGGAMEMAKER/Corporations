using Entitas;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public partial class Cooldowns
    {
        public static GameEntity[] GetCooldowns(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.TimedAction, GameMatcher.Cooldown));
        }

        public static bool HasCooldown(GameContext gameContext, CompanyTask task)
        {
            return HasTask(gameContext, task);
        }

        public static IEnumerable<GameEntity> GetCooldowns(GameContext gameContext, int companyId)
        {
            var cooldowns = GetCooldowns(gameContext);

            return cooldowns.Where(c => c.timedAction.CompanyTask.CompanyId == companyId);
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


        // simple cooldowns
        public static GameEntity GetSimpleCooldownContainer(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.SimpleCooldownContainer).First();
        }

        /// <summary>
        /// Adding simple cooldown has it's own style
        /// ex: $"company-{product.company.Id}-upgradeFeature-{featureName}";
        /// $"entity-{Id}-actionType-{attributes}";
        /// </summary>
        /// <param name="gameContext"></param>
        /// <param name="name"></param>
        /// <param name="duration"></param>
        public static void AddSimpleCooldown(GameContext gameContext, string name, int duration)
        {
            var date = ScheduleUtils.GetCurrentDate(gameContext);

            AddSimpleCooldown(gameContext, name, new SimpleCooldown { StartDate = date, EndDate = date + duration });
        }

        public static void AddSimpleCooldown(GameContext gameContext, string name, SimpleCooldown simpleCooldown)
        {
            var container = GetSimpleCooldownContainer(gameContext);

            container.simpleCooldownContainer.Cooldowns[name] = simpleCooldown;
        }

        public static bool HasCooldown(GameContext gameContext, string name, out SimpleCooldown simpleCooldown)
        {
            var container = GetSimpleCooldownContainer(gameContext);

            if (!container.simpleCooldownContainer.Cooldowns.ContainsKey(name))
            {
                simpleCooldown = null;
                return false;
            }

            simpleCooldown = container.simpleCooldownContainer.Cooldowns[name];
            return true;
        }
    }
}

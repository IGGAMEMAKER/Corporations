﻿using Assets.Classes;
using Entitas;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CooldownUtils
    {
        // new cooldwon system

        public static GameEntity GetCooldownContainer(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.CooldownContainer)[0];
        }

        public static Dictionary<string, Cooldown> GetCooldowns(GameContext gameContext)
        {
            var container = GetCooldownContainer(gameContext);

            return container.cooldownContainer.Cooldowns;
        }


        public static bool HasCooldown(GameContext gameContext, Cooldown cooldown)
        {
            return HasCooldown(gameContext, cooldown.GetKey());
        }

        public static bool HasCooldown(GameContext gameContext, string cooldownName)
        {
            return GetCooldowns(gameContext).ContainsKey(cooldownName);
        }



        public static void AddNewCooldown(GameContext gameContext, Cooldown cooldown, int duration)
        {
            var cooldownName = cooldown.GetKey();

            AddNewCooldown(gameContext, cooldownName, cooldown, duration);
        }
        public static void AddNewCooldown(GameContext gameContext, string cooldownName, Cooldown cooldown, int duration)
        {
            var c = GetCooldowns(gameContext);

            cooldown.EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration;

            c[cooldownName] = cooldown;
        }

        // specific cooldowns
        public static void AddConceptUpgradeCooldown(GameContext gameContext, GameEntity product, int duration)
        {
            AddNewCooldown(gameContext, new CooldownImproveConcept(product.company.Id), duration);
        }

        public static bool HasConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            return HasCooldown(gameContext, new CooldownImproveConcept(product.company.Id));
        }

        public static bool TryGetCooldown(GameContext gameContext, Cooldown req, out Cooldown cooldown)
        {
            return TryGetCooldown(gameContext, req.GetKey(), out cooldown);
        }

        public static bool TryGetCooldown(GameContext gameContext, string cooldownName, out Cooldown cooldown)
        {
            return GetCooldowns(gameContext).TryGetValue(cooldownName, out cooldown);
        }



        public static void AddTask(GameContext gameContext, CompanyTask companyTask)
        {
            var e = gameContext.CreateEntity();

            //e.AddTask(false, )
            companyTask.StartDate = ScheduleUtils.GetCurrentDate(gameContext);


        }
    }
}

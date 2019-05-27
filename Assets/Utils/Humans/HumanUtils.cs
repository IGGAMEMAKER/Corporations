using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class HumanUtils
    {
        public static GameEntity[] GetHumans(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Human);
        }

        public static int GenerateHumanId(GameContext gameContext)
        {
            return GetHumans(gameContext).Length;
        }

        public static GameEntity GetHumanById(GameContext gameContext, int humanId)
        {
            return Array.Find(GetHumans(gameContext), h => h.human.Id == humanId);
        }

        public static GameEntity GenerateHuman(GameContext gameContext)
        {
            int id = GenerateHumanId(gameContext);

            var e = gameContext.CreateEntity();
            e.AddHuman(id, "Tom", "Stokes " + id);

            return e;
        }

        public static GameEntity SetRole(GameEntity worker, WorkerRole workerRole)
        {
            return worker;
        }
    }
}

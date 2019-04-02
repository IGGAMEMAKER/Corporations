using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static class MenuUtils
    {
        public static GameEntity GetMenu (GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }
    }
}

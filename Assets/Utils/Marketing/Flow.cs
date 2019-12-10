using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            return Markets.GetClientFlow(gameContext, nicheType);
        }
    }
}

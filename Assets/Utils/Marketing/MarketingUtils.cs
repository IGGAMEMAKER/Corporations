using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static int GetClientLoyalty(GameContext gameContext, int companyId, NicheType nicheType)
        {
            return UnityEngine.Random.Range(-5, 25);
        }
    }
}

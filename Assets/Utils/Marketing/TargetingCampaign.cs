using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void EnableTargeting(GameEntity company)
        {
            company.isTargeting = true;
        }
    }
}

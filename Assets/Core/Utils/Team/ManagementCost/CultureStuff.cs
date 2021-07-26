using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static float GetPolicyValueModified(GameEntity company, CorporatePolicy policy, float min,
    float centerValue,
    float max)
        {
            var flatness = Companies.GetPolicyValue(company, policy);

            var center = 5;

            // ---- structure
            var multiplier = centerValue;
            if (flatness < center)
                multiplier = min;

            if (flatness > center)
                multiplier = max;

            return multiplier;
        }
    }
}

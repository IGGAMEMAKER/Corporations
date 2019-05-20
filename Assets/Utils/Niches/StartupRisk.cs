using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        static BonusContainer GetRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            int baseValue = Constants.RISKS_MONETISATION_MAX;
            int demand = GetMarketDemandRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            return new BonusContainer("Startup risk")
                .SetDimension("%")
                .Append("Is not profitable", baseValue)
                .Append("Unknown demand", demand)
                .Append("Strong competitors", competitors);
        }

        public static long GetStartupRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            return GetRiskOnNiche(gameContext, nicheType).Sum();
        }

        public static string GetStartupRiskOnNicheDescription(GameContext gameContext, NicheType nicheType)
        {
            var bonusContainer = GetRiskOnNiche(gameContext, nicheType);

            var risk = bonusContainer.Sum();

            string text = ShowRiskStatus(risk).ToString();

            return $"Current risk is {risk}%! ({text})" + bonusContainer.ToString(true);
        }

        public static int GetNewPlayerRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            // based on competitors level and amount
            return 33;
        }

        public static Risk ShowRiskStatus(long risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }
    }
}

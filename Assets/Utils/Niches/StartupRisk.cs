using System;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static int GetStartupRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            int baseValue = Constants.RISKS_MONETISATION_MAX;
            int demand = GetMarketDemandRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            return baseValue + demand + competitors;
        }

        public static string GetStartupRiskOnNicheDescription(GameContext gameContext, NicheType nicheType)
        {
            int risk = GetStartupRiskOnNiche(gameContext, nicheType);

            int baseValue = Constants.RISKS_MONETISATION_MAX;
            int demand = GetMarketDemandRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            BonusContainer bonusContainer = new BonusContainer("Startup risk", risk);

            bonusContainer
                .SetDimension("%")
                .Append("Base value", baseValue)
                .Append("Unknown demand", demand)
                .Append("Strong competitors", competitors);

            string text = ShowRiskStatus(risk).ToString();

            return $"Current risk is {risk}%! ({text})" + bonusContainer.ToString(true);
        }

        public static int GetNewPlayerRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            // based on competitors level and amount
            return 33;
        }

        public static Risk ShowRiskStatus(int risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }
    }
}

using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static int GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return -85;
        }

        public static int GetMonetisationRisk(GameContext gameContext, NicheType nicheType)
        {
            return 33;
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            // amount of users/niche fame
            return 33;
        }

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, int companyId)
        {
            return 33;
        }

        public static int GetNewPlayerRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            // based on competitors level and amount
            return 33;
        }

        public static int GetStartupRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            return
                GetMonetisationRisk(gameContext, nicheType) +
                GetMarketDemandRisk(gameContext, nicheType) +
                GetNewPlayerRiskOnNiche(gameContext, nicheType);
        }

        public static Risk ShowRiskStatus(int risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }

        public static string GetStartupRiskOnNicheDescription(GameContext gameContext, NicheType nicheType)
        {
            int risk = GetStartupRiskOnNiche(gameContext, nicheType);
            string text = ShowRiskStatus(risk).ToString();

            int demand = GetMarketDemandRisk(gameContext, nicheType);
            int paymentAbility = GetMonetisationRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            return $"Current risk is {risk}%! ({text})" +
            $"\nUnknown demand: +{demand}%" +
            $"\nUnknown payments: +{paymentAbility}%" +
            $"\nStrong competitors: +{competitors}%";
        }
    }
}

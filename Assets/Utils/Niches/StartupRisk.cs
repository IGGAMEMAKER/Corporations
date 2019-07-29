using Entitas;
using System;
using System.Collections;
using System.Collections.Generic;

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
                .Append("Niche demand risk", demand)
                .Append("Competitors", competitors);
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

        public static string GetCompanyPositioning(GameEntity company, GameContext gameContext)
        {
            var positioning = company.productPositioning.Positioning;
            var posTextual = NicheUtils.GetNichePositionings(company.product.Niche, gameContext)[positioning].name;

            return posTextual;
        }

        internal static Dictionary<int, ProductPositioning> GetNichePositionings(NicheType niche, GameContext gameContext)
        {
            var e = GetNicheEntity(gameContext, niche);

            var p = e.nicheSegments.Positionings;

            return p;
        }

        public static int GetNewPlayerRiskOnNiche(GameContext gameContext, NicheType nicheType)
        {
            // based on competitors level and amount
            return 33;
        }

        internal static object GetMaintenanceCost(GameEntity niche)
        {
            var costs = GetNicheCosts(niche);

            var branding = costs.AdCost * 10 * 5;
            var targeting = costs.AdCost;

            var marketingCost = targeting;

            return marketingCost;
        }

        public static Risk ShowRiskStatus(long risk)
        {
            if (risk < 10)
                return Risk.Guaranteed;

            if (risk < 50)
                return Risk.Risky;

            return Risk.TooRisky;
        }

        
        internal static GameEntity[] GetInstitutionalInvestors(GameContext gameContext, GameEntity e)
        {
            return Array.FindAll(gameContext
                .GetEntities(GameMatcher.Shareholder),
                s => CompanyUtils.IsInSphereOfInterest(s, e.niche.NicheType)
                );
        }
    }
}

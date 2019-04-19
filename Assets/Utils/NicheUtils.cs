using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static class NicheUtils
    {

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetPlayersOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, GameEntity e)
        {
            return GetPlayersOnMarket(context, e.product.Niche);
        }

        public static IEnumerable<GameEntity> GetPlayersOnMarket(GameContext context, NicheType niche)
        {
            return context.GetEntities(GameMatcher.Product).Where(p => p.product.Niche == niche);
        }

        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorInfo(GameEntity e, GameContext context)
        {
            var names = GetPlayersOnMarket(context, e)
                .Select(c => c.product.ProductLevel + "lvl - " + ProlongNameToNDigits(c.product.Name, 10));

            return names;
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetPlayersOnMarket(context, niche).Count();
        }

        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(context.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            return Array.FindAll(context.GetEntities(GameMatcher.Niche), n => n.niche.IndustryType == industry);
        }

        public static int GetPaymentAbilityRisk(GameContext gameContext, NicheType nicheType)
        {
            return 33;
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            // amount of users/niche fame
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
                GetPaymentAbilityRisk(gameContext, nicheType) +
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
            int paymentAbility = GetPaymentAbilityRisk(gameContext, nicheType);
            int competitors = GetNewPlayerRiskOnNiche(gameContext, nicheType);

            return $"Current risk is {risk}%! ({text})" +
            $"\nUnknown demand: +{demand}%" +
            $"\nUnknown payments: +{paymentAbility}%" +
            $"\nStrong competitors: +{competitors}%";
        }

        public static bool IsBestAppOnNiche(GameContext gameContext, int companyId)
        {
            return GetLeaderApp(gameContext, companyId).company.Id == companyId;
        }

        //public static bool IsBestAppOnNiche(GameContext gameContext, int companyId)
        //{
        //    return GetLeaderApp(gameContext, companyId).company.Id == companyId;
        //}

        public static int GetCompetititiveRiskOnNiche(GameContext gameContext, int companyId)
        {
            return 33;
        }

        private static GameEntity GetBestApp(IEnumerable<GameEntity> apps)
        {
            GameEntity best = null;

            foreach (var p in apps)
            {
                if (best == null)
                    best = p;

                if (p.product.ProductLevel > best.product.ProductLevel)
                    best = p;
            }

            return best;
        }

        public static GameEntity GetLeaderApp(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetLeaderApp(gameContext, c.product.Niche);
        }

        public static GameEntity GetLeaderApp(GameContext gameContext, NicheType nicheType)
        {
            var competingProducts = GetPlayersOnMarket(gameContext, nicheType);

            return GetBestApp(competingProducts);
        }
    }
}

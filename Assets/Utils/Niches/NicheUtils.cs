using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static GameEntity GetNicheEntity(GameContext gameContext, NicheType nicheType)
        {
            return Array.Find(gameContext.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == nicheType);
        }

        public static GameEntity[] GetNiches(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Niche);
        }

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

        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorSegmentLevels(GameEntity e, GameContext context, UserType userType)
        {
            var names = GetPlayersOnMarket(context, e)
                .Select(c => c.product.Segments[userType] + "lvl - " + ProlongNameToNDigits(c.company.Name, 10));

            return names;
        }

        public static int GetMinimumPhaseDurationModifier(NicheLifecyclePhase phase)
        {
            switch (phase)
            {
                case NicheLifecyclePhase.Innovation:
                    return 1;

                case NicheLifecyclePhase.Trending:
                    return 4;

                case NicheLifecyclePhase.MassUse:
                    return 10;

                case NicheLifecyclePhase.Decay:
                    return 15;

                default:
                    return 0;
            }
        }

        public static GameEntity[] GetCompetitorsOfCompany(GameContext context, GameEntity company)
        {
            return context
                .GetEntities(GameMatcher.Product)
                .Where(c =>
                // same niche
                c.product.Niche == company.product.Niche &&
                // get competitors only
                c.company.Id != company.company.Id)
                .ToArray();
        }

        public static IndustryType GetIndustry(NicheType niche, GameContext context)
        {
            return Array.Find(context.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == niche).niche.IndustryType;
        }

        public static GameEntity[] GetNichesInIndustry(IndustryType industry, GameContext context)
        {
            return Array.FindAll(context.GetEntities(GameMatcher.Niche), n => n.niche.IndustryType == industry);
        }
    }
}

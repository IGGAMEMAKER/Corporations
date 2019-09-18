using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static IEnumerable<GameEntity> GetNonFinancialCompaniesWithSameInterests (GameContext context, GameEntity company)
        {
            return GetCompaniesWithSameInterests(context, company).Where(CompanyUtils.IsNotFinancialStructure);
        }

        public static IEnumerable<GameEntity> GetCompaniesWithSameInterests (GameContext context, GameEntity company)
        {
            var independent = CompanyUtils.GetIndependentCompanies(context);

            var interests = company.companyFocus.Niches;

            return independent.Where(c =>
            (c.hasProduct && interests.Contains(c.product.Niche))
            ||
            (c.hasCompanyFocus && c.companyFocus.Niches.Intersect(interests).Count() > 0)
            );
        }

        public static IEnumerable<GameEntity> GetCompaniesInterestedInMarket (GameContext context, NicheType niche)
        {
            var independent = CompanyUtils.GetIndependentCompanies(context);

            return independent.Where(c =>
            (c.hasProduct && c.product.Niche == niche)
            ||
            (c.hasCompanyFocus && c.companyFocus.Niches.Contains(niche))
            );
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            return GetProductsOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, GameEntity product)
        {
            return GetProductsOnMarket(context, product.product.Niche);
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, NicheType niche)
        {
            return CompanyUtils.GetProductCompanies(context).Where(p => p.product.Niche == niche);
        }

        public static GameEntity[] GetProductsOnMarket(GameContext context, NicheType niche, bool something)
        {
            return GetProductsOnMarket(context, niche).ToArray();
        }

        public static int GetCompetitorsAmount(GameEntity e, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetProductsOnMarket(context, e).Count();
        }

        public static int GetCompetitorsAmount(NicheType niche, GameContext context)
        {
            // returns amount of competitors on specific niche

            return GetProductsOnMarket(context, niche).Count();
        }




        internal static BonusContainer GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            var isOutdated = ProductUtils.IsOutOfMarket(company, gameContext);
            var isInMarket = ProductUtils.IsInMarket(company, gameContext);

            return new BonusContainer("Product Competitiveness")
                .RenderTitle()
                .AppendAndHideIfZero("Is in market", isInMarket ? 2 : 0)
                .AppendAndHideIfZero("Is outdated", isOutdated ? - 10: 0)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus);
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitivenessBonus(company, gameContext).Sum();
        }


        static string ProlongNameToNDigits(string name, int n)
        {
            if (name.Length >= n) return name.Substring(0, n - 3) + "...";

            return name;
        }

        public static IEnumerable<string> GetCompetitorSegmentLevels(GameEntity e, GameContext context)
        {
            var names = GetProductsOnMarket(context, e)
                .Select(c => c.product.Concept + "lvl - " + ProlongNameToNDigits(c.company.Name, 10));

            return names;
        }
    }
}

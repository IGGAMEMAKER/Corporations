using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, int companyId)
        {
            var c = CompanyUtils.GetCompany(context, companyId);

            return GetProductsOnMarket(context, c);
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameEntity niche, GameContext context)
        {
            return GetProductsOnMarket(context, niche.niche.NicheType);
        }
        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, GameEntity product)
        {
            return GetProductsOnMarket(context, product.product.Niche);
        }

        public static GameEntity[] GetProductsOnMarket(GameContext context, NicheType niche, bool something)
        {
            return GetProductsOnMarket(context, niche).ToArray();
        }

        public static IEnumerable<GameEntity> GetProductsOnMarket(GameContext context, NicheType niche)
        {
            return CompanyUtils.GetProductCompanies(context).Where(p => p.product.Niche == niche);
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



        // sphere of interests
        public static IEnumerable<GameEntity> GetNonFinancialCompaniesWithSameInterests (GameContext context, GameEntity company)
        {
            return GetCompaniesWithSameInterests(context, company).Where(CompanyUtils.IsNotFinancialStructure);
        }

        public static IEnumerable<GameEntity> GetFinancialStructuresWithSameInterests (GameContext context, GameEntity company)
        {
            return GetCompaniesWithSameInterests(context, company).Where(CompanyUtils.IsFinancialStructure);
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

        public static bool IsShareSameInterests(GameEntity c1, GameEntity c2)
        {
            var interests1 = c1.companyFocus.Niches;
            var interests2 = c2.companyFocus.Niches;

            return interests1.Intersect(interests2).Count() > 0;
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



        public static bool IsEmptyMarket(GameContext gameContext, GameEntity niche)
        {
            return GetProductsOnMarket(gameContext, niche.niche.NicheType).Count() == 0;
        }

        // Leaders
        public static GameEntity GetMostProfitableCompanyOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderByDescending(p => EconomyUtils.GetProfit(p, context))
                .FirstOrDefault();

            return productCompany;
        }

        public static long GetBiggestIncomeOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderByDescending(p => EconomyUtils.GetProductCompanyIncome(p, context))
                .FirstOrDefault();

            if (productCompany == null)
                return 0;

            return EconomyUtils.GetProductCompanyIncome(productCompany, context);
        }

        public static long GetLowestIncomeOnMarket(GameContext context, GameEntity niche)
        {
            var players = GetProductsOnMarket(context, niche.niche.NicheType);

            var productCompany = players
                .OrderBy(p => EconomyUtils.GetProductCompanyIncome(p, context))
                .FirstOrDefault();

            if (productCompany == null)
                return 0;

            return EconomyUtils.GetProductCompanyIncome(productCompany, context);
        }

        public static GameEntity GetPotentialMarketLeader(GameContext context, NicheType niche)
        {
            var list = GetProductsOnMarket(context, niche)
            .OrderByDescending(p => ProductUtils.GetInnovationChance(p, context) * 100 + (int)p.branding.BrandPower);

            if (list.Count() == 0)
                return null;

            return list.First();
        }



        // competitiveness
        internal static Bonus<long> GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            var conceptStatus = ProductUtils.GetConceptStatus(company, gameContext);

            var techLeadershipBonus = conceptStatus == ConceptStatus.Leader;

            var isOutdated = conceptStatus == ConceptStatus.Outdated;
            var isInMarket = conceptStatus == ConceptStatus.Relevant;

            return new Bonus<long>("Product Competitiveness")
                .RenderTitle()
                .AppendAndHideIfZero("Is in market", isInMarket ? 2 : 0)
                .AppendAndHideIfZero("Is outdated", isOutdated ? - 10: 0)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus ? 15 : 0);
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return (long)GetProductCompetitivenessBonus(company, gameContext).Sum();
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

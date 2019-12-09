using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static GameEntity[] GetDaughterCompaniesRecursively(GameContext context, int companyId)
        {
            return GetDaughterCompanies(context, companyId);
        }

        public static GameEntity[] GetDaughterProductCompanies(GameContext context, GameEntity company)
        {
            return GetDaughterCompanies(context, company)
                .Where(p => p.hasProduct)
                .ToArray();
        }

        public static GameEntity[] GetDaughterCompaniesOnMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            return GetDaughterProductCompanies(gameContext, group)
                .Where(p => p.product.Niche == nicheType)
                .ToArray();
        }

        public static GameEntity[] GetDaughterCompanies(GameContext context, int companyId) => GetDaughterCompanies(context, GetCompany(context, companyId));
        public static GameEntity[] GetDaughterCompanies(GameContext context, GameEntity c)
        {
            if (!c.hasShareholder)
                return new GameEntity[0];

            int investorId = c.shareholder.Id;

            return GetInvestments(context, investorId);
        }

        public static bool IsDaughterOfCompany(GameEntity parent, GameEntity daughter)
        {
            return IsInvestsInCompany(daughter, parent.shareholder.Id);
        }

        public static GameEntity GetParentCompany(GameContext context, GameEntity company)
        {
            if (company.isIndependentCompany)
                return null;


            var shares = 0;
            var shareholders = company.shareholders.Shareholders;
            GameEntity parent = null;

            foreach (var shareholder in shareholders)
            {
                var id = shareholder.Key;
                var block = shareholder.Value.amount;

                var investor = InvestmentUtils.GetInvestorById(context, id);
                if (investor.hasCompany)
                {
                    // is managing company
                    if (block > shares)
                    {
                        parent = investor;
                    }
                }
            }

            return parent;
        }

        private static GameEntity[] GetInvestments(GameContext context, int investorId)
        {
            return Array.FindAll(
                context.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders)),
                c => IsInvestsInCompany(c, investorId)
                );
        }

        public static List<CompanyHolding> GetCompanyHoldings(GameContext context, int companyId, bool recursively)
        {
            List<CompanyHolding> companyHoldings = new List<CompanyHolding>();

            var c = GetCompany(context, companyId);

            var investments = GetDaughterCompanies(context, companyId);

            foreach (var investment in investments)
            {
                var holding = new CompanyHolding
                {
                    companyId = investment.company.Id,
                    control = GetShareSize(context, investment.company.Id, c.shareholder.Id),
                    holdings = recursively ? GetCompanyHoldings(context, investment.company.Id, recursively) : new List<CompanyHolding>()
                };

                companyHoldings.Add(holding);
            }

            return companyHoldings;
        }

        public static List<CompanyHolding> GetPersonalHoldings(GameContext context, int shareholderId, bool recursively)
        {
            List<CompanyHolding> companyHoldings = new List<CompanyHolding>();

            var investments = InvestmentUtils.GetInvestmentsOf(context, shareholderId);

            foreach (var investment in investments)
            {
                var holding = new CompanyHolding
                {
                    companyId = investment.company.Id,
                    control = GetShareSize(context, investment.company.Id, shareholderId),
                    holdings = recursively ? GetCompanyHoldings(context, investment.company.Id, recursively) : new List<CompanyHolding>()
                };

                companyHoldings.Add(holding);
            }

            return companyHoldings;
        }


    }

    public class CompanyHolding
    {
        // shares percent
        public int control;

        // controlled company id
        public int companyId;

        public List<CompanyHolding> holdings;
    }
}

using Entitas;
using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    public class CompanyHolding
    {
        // shares percent
        public int control;

        // controlled company id
        public int companyId;

        public List<CompanyHolding> holdings;
    }

    public class CompanyTree
    {
        // root company id
        public int companyId;

        public List<CompanyHolding> holdings;
    }

    public static partial class CompanyUtils
    {
        public static GameEntity[] GetDaughterCompaniesRecursively(GameContext context, int companyId)
        {
            return GetDaughterCompanies(context, companyId);
        }

        public static GameEntity[] GetDaughterCompanies(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (!c.hasShareholder)
                return new GameEntity[0];

            int investorId = c.shareholder.Id;

            return GetInvestments(context, investorId);
        }

        public static bool IsDaughterOfCompany(GameEntity parent, GameEntity daughter)
        {
            return daughter.shareholders.Shareholders.ContainsKey(parent.shareholder.Id);
        }

        public static List<CompanyHolding> GetCompanyHoldings(GameContext context, int companyId, bool recursively)
        {
            List<CompanyHolding> companyHoldings = new List<CompanyHolding>();

            var c = GetCompanyById(context, companyId);

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
}

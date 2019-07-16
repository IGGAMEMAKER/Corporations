using System;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void PayDividends(GameContext gameContext, int companyId)
        {
            PayDividends(gameContext, GetCompanyById(gameContext, companyId));
        }

        public static void PayDividends(GameContext gameContext, GameEntity company)
        {
            int dividendSize = 33;
            var balance = company.companyResource.Resources.money;

            var dividends = balance * dividendSize / 100;

            PayDividends(gameContext, company, dividends);
        }

        internal static List<AcquisitionOfferComponent> GetAcquisitionOffers(GameContext gameContext, int id)
        {
            var proposals = new List<AcquisitionOfferComponent>
            {
                new AcquisitionOfferComponent {
                    CompanyId = id,
                    BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
                    Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, id)
                }
            };

            foreach (var c in CompanyUtils.GetDaughterCompanies(gameContext, id))
            {
                proposals.Add(new AcquisitionOfferComponent
                {
                    CompanyId = c.company.Id,
                    BuyerId = InvestmentUtils.GetRandomInvestmentFund(gameContext),
                    Offer = CompanyEconomyUtils.GetCompanyCost(gameContext, c.company.Id)
                });
            }

            return proposals;
        }

        public static void PayDividends(GameContext gameContext, GameEntity company, long dividends)
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var investorId = s.Key;
                var sharePercentage = GetShareSize(gameContext, company.company.Id, investorId);

                var sum = dividends * sharePercentage / 100;

                AddMoneyToInvestor(gameContext, investorId, sum);
            }

            SpendResources(company, new Classes.TeamResource(dividends));
        }
    }
}

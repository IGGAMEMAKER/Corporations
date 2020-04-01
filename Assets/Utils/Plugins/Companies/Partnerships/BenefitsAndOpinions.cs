using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static Bonus<long> GetPartnerability(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            return new Bonus<long>("Partnership possibility")
                .Append("Base", -1)
                .AppendAndHideIfZero("Have competing products", IsHaveCompetingProducts(requester, acceptor, gameContext) ? -400 : 0)
                .AppendAndHideIfZero("Have common markets", IsHaveIntersectingMarkets(requester, acceptor, gameContext) ? -190 : 0)
                .AppendAndHideIfZero("Max amount of partners", IsHasTooManyPartnerships(acceptor) ? -75 : 0)
                .AppendAndHideIfZero("You have partnerships with their competitors", IsPartnerOfCompetingCompany(requester, acceptor, gameContext) ? -200 : 0)
                .Append("Partnership benefits", (long)GetCompanyBenefitFromTargetCompany(acceptor, requester, gameContext))
                //.Append("Partnership benefits", (long)GetCompanyBenefitFromTargetCompany(requester, acceptor, gameContext))
                ;
        }

        public static long GetPartnershipOpinionAboutUs(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            return GetPartnerability(requester, acceptor, gameContext).Sum();
        }

        public static float GetCompanyBenefitFromTargetCompany(GameEntity company, GameEntity target, GameContext gameContext)
        {
            var targetManager = GetManagingCompanyOf(target, gameContext);
            var sameIndustries = company.companyFocus.Industries.Intersect(targetManager.companyFocus.Industries);

            return sameIndustries
                .Sum(i => GetCompanyStrengthInIndustry(targetManager, i, gameContext))
                ;
        }


        public static float GetCompanyStrengthInIndustry(GameEntity company, IndustryType industry, GameContext gameContext)
        {
            if (company.isManagingCompany)
            {
                var daughtersInIndustry = GetDaughterProductCompanies(gameContext, company)
                    .Where(p => Markets.GetIndustry(p.product.Niche, gameContext) == industry);

                return daughtersInIndustry
                    .Sum(d => d.branding.BrandPower);
            }
            else
            {
                // product company
                var ind = Markets.GetIndustry(company.product.Niche, gameContext);

                return ind == industry ? company.branding.BrandPower : 0;
            }
        }
    }
}

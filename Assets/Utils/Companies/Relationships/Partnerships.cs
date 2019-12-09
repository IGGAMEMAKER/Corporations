using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static Bonus<long> GetPartnerability(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            return new Bonus<long>("Partnership possibility")
                .Append("Base", -1)
                .AppendAndHideIfZero("Have competing products", IsHaveCompetingProducts(requester, acceptor, gameContext) ? -100 : 0)
                .AppendAndHideIfZero("Have common markets", IsHaveIntersectingMarkets(requester, acceptor, gameContext) ? -90 : 0)
                .AppendAndHideIfZero("Max amount of partners", IsHasTooManyPartnerships(acceptor) ? -75 : 0)
                .AppendAndHideIfZero("You have partnerships with their competitors", IsPartnerOfCompetingCompany(requester, acceptor, gameContext) ? -200 : 0)
                .Append("Partnership benefits", (long)GetCompanyBenefitFromTargetCompany(acceptor, requester, gameContext))
                //.Append("Partnership benefits", (long)GetCompanyBenefitFromTargetCompany(requester, acceptor, gameContext))
                ;
        }

        public static bool IsCompetingCompany(int companyId1, int companyId2, GameContext gameContext) => IsCompetingCompany(GetCompany(gameContext, companyId1), GetCompany(gameContext, companyId2), gameContext);
        public static bool IsCompetingCompany(GameEntity company1, GameEntity company2, GameContext gameContext)
        {
            return IsHaveCompetingProducts(company1, company2, gameContext) || IsHaveIntersectingMarkets(company1, company2, gameContext);
        }

        public static bool IsPartnerOfCompetingCompany(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            foreach (var requesterPartner in requester.partnerships.companies)
            {
                // signing contract will piss one of acceptor partners
                if (IsCompetingCompany(acceptor.company.Id, requesterPartner, gameContext))
                    return true;
            }

            return false;
        }

        public static void SendStrategicPartnershipRequest(GameEntity requester, GameEntity acceptor, GameContext gameContext, bool notifyPlayer)
        {
            // don't render buttons
            if (!IsCanBePartnersTheoretically(requester, acceptor))
                return;

            // don't render buttons
            if (IsHaveStrategicPartnershipAlready(requester, acceptor))
                return;

            if (IsHasTooManyPartnerships(requester) || IsHasTooManyPartnerships(acceptor))
                return;




            var acceptorStrength = acceptor.branding.BrandPower;
            var requesterStrength = requester.branding.BrandPower;

            bool wantsToAccept = true; // Mathf.Abs(acceptorStrength - requesterStrength) < 10;

            if (wantsToAccept)
            {
                acceptor.partnerships.companies.Add(requester.company.Id);
                requester.partnerships.companies.Add(acceptor.company.Id);
            }

            if (notifyPlayer)
                NotifyAboutPartnershipResponse(requester, acceptor, wantsToAccept, gameContext);
        }

        public static float GetCompanyBenefitFromTargetCompany(GameEntity company, GameEntity target, GameContext gameContext)
        {
            var sameIndustries = company.companyFocus.Industries.Intersect(target.companyFocus.Industries);

            return sameIndustries
                .Sum(i => GetCompanyStrengthInIndustry(target, i, gameContext))
                ;
        }

        public static float GetCompanyStrengthInIndustry(GameEntity company, IndustryType industry, GameContext gameContext)
        {
            if (company.isManagingCompany)
            {
                var daughtersInIndustry = GetDaughterProductCompanies(gameContext, company)
                    .Where(p => NicheUtils.GetIndustry(p.product.Niche, gameContext) == industry);

                return daughtersInIndustry
                    .Sum(d => d.branding.BrandPower);
            }
            else
            {
                // product company
                var ind = NicheUtils.GetIndustry(company.product.Niche, gameContext);

                return ind == industry ? company.branding.BrandPower : 0;
            }
        }

        public static int GetPartnershipBenefits(GameEntity requester, GameEntity acceptor)
        {
            var acceptorStrength = acceptor.branding.BrandPower;
            var requesterStrength = requester.branding.BrandPower;

            var equalityZone = 10;

            return (int)(requesterStrength - acceptorStrength) + equalityZone;
        }

        public static void CancelStrategicPartnership(GameEntity requester, GameEntity acceptor)
        {
            acceptor.partnerships.companies.RemoveAll(id => id == requester.company.Id);
            requester.partnerships.companies.RemoveAll(id => id == acceptor.company.Id);
        }

        public static void NotifyAboutPartnershipResponse(GameEntity requester, GameEntity acceptor, bool willAccept, GameContext gameContext)
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageStrategicPartnership(requester.company.Id, acceptor.company.Id, willAccept));
        }


        // validation
        public static bool IsHasTooManyPartnerships(GameEntity company)
        {
            var maxPartnerships = 3;

            return company.partnerships.companies.Count >= maxPartnerships;
        }

        public static bool IsHaveStrategicPartnershipAlready(GameEntity c1, GameEntity c2)
        {
            return
                c1.partnerships.companies.Contains(c2.company.Id) &&
                c2.partnerships.companies.Contains(c1.company.Id);
        }

        public static bool IsCanBePartnersTheoretically(GameEntity requester, GameEntity acceptor)
        {
            // self partnering :)
            if (requester.company.Id == acceptor.company.Id)
                return false;

            if (!(requester.isIndependentCompany && acceptor.isIndependentCompany))
                return false;

            return true;
        }

        public static bool IsHaveCompetingProducts(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            var requesterMarkets = GetParticipatingMarkets(requester, gameContext);
            var acceptorMarkets = GetParticipatingMarkets(acceptor, gameContext);

            var competingProducts = requesterMarkets.Intersect(acceptorMarkets);

            return competingProducts.Count() > 0;
        }

        public static bool IsHaveIntersectingMarkets(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            var commonMarkets = requester.companyFocus.Niches.Intersect((acceptor.companyFocus.Niches));

            return commonMarkets.Count() > 0;
        }

        public static NicheType[] GetParticipatingMarkets(GameEntity company, GameContext gameContext)
        {
            if (company.hasProduct)
                return new NicheType[1] { company.product.Niche };

            var daughters = GetDaughterCompanies(gameContext, company.company.Id);

            return daughters
                .Where(d => d.hasProduct)
                .Select(d => d.product.Niche)
                .ToArray();
        }



        public static GameEntity[] GetPartnershipCandidates (GameEntity company, GameContext gameContext)
        {
            return GetIndependentCompanies(gameContext)
                .Where(IsNotFinancialStructure)
                .Where(c => IsCanBePartnersTheoretically(company, c))
                .Where(c => !IsHaveStrategicPartnershipAlready(company, c))
                //.Where(c => CompanyUtils.IsHaveIntersectingMarkets(MyCompany, c, GameContext))
                .ToArray()
                ;
        }

        public static GameEntity[] GetPartnershipCandidatesWhoWantToBePartnersWithUs(GameEntity company, GameContext gameContext)
        {
            var candidates = GetPartnershipCandidates(company, gameContext);

            return candidates
                .Where(c => GetPartnerability(company, c, gameContext).Sum() > 0)
                .OrderByDescending(c => GetCompanyBenefitFromTargetCompany(company, c, gameContext))
                .ToArray();
        }



        public static List<int> GetPartnersOf(GameEntity company, GameContext gameContext)
        {
            if (company.isIndependentCompany)
                return company.partnerships.companies;

            var parent = GetParentCompany(gameContext, company);

            return parent.partnerships.companies;
        }

        public static List<GameEntity> GetPartnerList(GameEntity company, GameContext gameContext)
        {
            var partners = GetPartnersOf(company, gameContext);

            return partners
                .Select(p => GetCompany(gameContext, p))
                .Where(p => p.hasProduct)
                .ToList();
        }
    }
}

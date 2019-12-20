using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        public static void SendStrategicPartnershipRequest(GameEntity requester, GameEntity acceptor, GameContext gameContext, bool notifyPlayer)
        {
            // don't render buttons
            if (!IsCanBePartnersTheoretically(requester, acceptor) || IsHaveStrategicPartnershipAlready(requester, acceptor))
                return;

            if (IsHasTooManyPartnerships(requester) && notifyPlayer)
            {
                NotificationUtils.AddPopup(gameContext, new PopupMessageTooManyPartners(requester.company.Id));
                return;
            }

            if (IsHasTooManyPartnerships(acceptor) && notifyPlayer)
            {
                NotificationUtils.AddPopup(gameContext, new PopupMessageTooManyPartners(acceptor.company.Id));
                return;
            }




            bool wantsToAccept = GetPartnershipOpinionAboutUs(requester, acceptor, gameContext) > 0;

            if (wantsToAccept)
                AcceptStrategicPartnership(requester, acceptor);

            if (notifyPlayer)
                NotifyAboutPartnershipResponse(requester, acceptor, wantsToAccept, gameContext);
        }

        public static int[] GetPartnershipCopy(GameEntity company)
        {
            int[] ids = new int[company.partnerships.companies.Count];
            company.partnerships.companies.CopyTo(ids);

            return ids;
        }

        public static void RemoveAllPartnerships(GameEntity company, GameContext gameContext)
        {
            foreach (var p in GetPartnershipCopy(company))
            {
                var acceptor = GetCompany(gameContext, p);

                BreakStrategicPartnership(company, acceptor);
                // notify about breaking partnership
            }
        }

        public static void AcceptStrategicPartnership(GameEntity requester, GameEntity acceptor)
        {
            acceptor.partnerships.companies.Add(requester.company.Id);
            requester.partnerships.companies.Add(acceptor.company.Id);
        }

        public static void BreakStrategicPartnership(GameEntity requester, GameEntity acceptor)
        {
            acceptor.partnerships.companies.RemoveAll(id => id == requester.company.Id);
            requester.partnerships.companies.RemoveAll(id => id == acceptor.company.Id);
        }


        public static void NotifyAboutPartnershipResponse(GameEntity requester, GameEntity acceptor, bool willAccept, GameContext gameContext)
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageStrategicPartnership(requester.company.Id, acceptor.company.Id, willAccept));
        }


        // queries
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
                .Where(c => GetPartnershipOpinionAboutUs(company, c, gameContext) > 0)
                .OrderByDescending(c => GetCompanyBenefitFromTargetCompany(company, c, gameContext))
                .ToArray();
        }



        public static List<int> GetPartnersOf(GameEntity company, GameContext gameContext)
        {
            var manager = GetManagingCompanyOf(company, gameContext);

            return manager.partnerships.companies;
        }

        public static List<GameEntity> GetPartnerList(GameEntity company, GameContext gameContext)
        {
            var partners = GetPartnersOf(company, gameContext);

            return partners
                .Select(p => GetCompany(gameContext, p))
                //.Where(p => p.hasProduct)
                .ToList();
        }

        public static float GetBrandProjectionOnIndustry(GameEntity company, GameContext gameContext, NicheType nicheType)
        {
            var industry = Markets.GetIndustry(nicheType, gameContext);

            return GetBrandProjectionOnIndustry(company, gameContext, industry);
        }
        public static float GetBrandProjectionOnIndustry(GameEntity company, GameContext gameContext, IndustryType industryType)
        {
            //var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext);
            //var marketSize = Markets.GetMarketRating(gameContext, product.product.Niche);

            //value += 1; // marketShare * marketSize / 100f;
            return 1;
        }
    }
}

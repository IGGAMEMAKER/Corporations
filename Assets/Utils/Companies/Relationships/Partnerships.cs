using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static Bonus<long> GetPartnerability(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            return new Bonus<long>("Partnership possibility")
                .AppendAndHideIfZero("Have competing products", IsHaveCompetingProducts(requester, acceptor, gameContext) ? -100 : 0)
                .AppendAndHideIfZero("Have common markets", IsHaveIntersectingMarkets(requester, acceptor, gameContext) ? -90 : 0)
                .AppendAndHideIfZero("Max amount of partners", IsHasTooManyPartnerships(acceptor) ? -75 : 0)
                .Append("Partnership benefits", GetPartnershipBenefits(requester, acceptor))
                ;
        }

        public static void SendStrategicPartnershipRequest(GameEntity requester, GameEntity acceptor, GameContext gameContext, bool notifyPlayer = false)
        {
            // don't render buttons
            if (!IsCanBePartnersTheoretically(requester, acceptor))
                return;

            // don't render buttons
            if (IsHaveStrategicPartnership(requester, acceptor))
                return;

            if (IsHasTooManyPartnerships(requester) || IsHasTooManyPartnerships(acceptor))
                return;




            var acceptorStrength = acceptor.branding.BrandPower;
            var requesterStrength = requester.branding.BrandPower;

            bool wantsToAccept = true; // Mathf.Abs(acceptorStrength - requesterStrength) < 10;

            if (wantsToAccept)
            {
                acceptor.partnerships.Companies.Add(requester.company.Id);
                requester.partnerships.Companies.Add(acceptor.company.Id);
            }

            if (notifyPlayer)
                NotifyAboutPartnershipResponse(requester, acceptor, gameContext);
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
            acceptor.partnerships.Companies.RemoveAll(id => id == requester.company.Id);
            requester.partnerships.Companies.RemoveAll(id => id == acceptor.company.Id);
        }

        public static void NotifyAboutPartnershipResponse(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            NotificationUtils.AddPopup(gameContext, new PopupMessageStrategicPartnership(requester.company.Id, acceptor.company.Id, true));
        }


        // validation
        public static bool IsHasTooManyPartnerships(GameEntity company)
        {
            var maxPartnerships = 3;

            return company.partnerships.Companies.Count >= maxPartnerships;
        }

        public static bool IsHaveStrategicPartnership(GameEntity c1, GameEntity c2)
        {
            return
                c1.partnerships.Companies.Contains(c2.company.Id) &&
                c2.partnerships.Companies.Contains(c1.company.Id);
        }

        public static bool IsCanBePartnersTheoretically(GameEntity requester, GameEntity acceptor)
        {
            // self partnering :)
            if (requester.company.Id == acceptor.company.Id)
                return false;

            if (!requester.isIndependentCompany ||
                !acceptor.isIndependentCompany)
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








        public static List<int> GetPartnersOf(GameEntity company, GameContext gameContext)
        {
            if (company.isIndependentCompany)
                return company.partnerships.Companies;

            var parent = GetParentCompany(gameContext, company);

            return parent.partnerships.Companies;
        }

        public static List<GameEntity> GetPartnerList(GameEntity company, GameContext gameContext)
        {
            var partners = GetPartnersOf(company, gameContext);

            return partners
                .Select(p => GetCompanyById(gameContext, p))
                .Where(p => p.hasProduct)
                .ToList();
        }
    }
}

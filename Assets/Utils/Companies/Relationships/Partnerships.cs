using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void SendStrategicPartnershipRequest(GameEntity requester, GameEntity acceptor, bool notifyPlayer = false)
        {
            if (!IsCanBePartnersTheoretically(requester, acceptor))
                return;

            if (IsHaveStrategicPartnership(requester, acceptor))
                return;

            var maxPartnerships = 3;
            if (requester.partnerships.Companies.Count >= maxPartnerships ||
                acceptor.partnerships.Companies.Count >= maxPartnerships)
                return;


            var acceptorStrength = acceptor.branding.BrandPower;
            var requesterStrength = requester.branding.BrandPower;

            bool wantsToAccept = Mathf.Abs(acceptorStrength - requesterStrength) < 10;

            if (wantsToAccept)
            {
                acceptor.partnerships.Companies.Add(requester.company.Id);
                requester.partnerships.Companies.Add(acceptor.company.Id);
            }

            //if (notifyPlayer)
            //    NotifyAboutPartnershipResponse()
        }

        public static bool IsHaveStrategicPartnership(GameEntity c1, GameEntity c2)
        {
            return
                c1.partnerships.Companies.Contains(c2.company.Id) ||
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

        public static bool IsHaveIntersectingMarketsOrProducts(GameEntity requester, GameEntity acceptor, GameContext gameContext)
        {
            var isIntersectingMarkets = requester.companyFocus.Niches.Intersect((acceptor.companyFocus.Niches)).Count() > 0;

            var requesterMarkets = GetParticipatingMarkets(requester, gameContext);
            var acceptorMarkets = GetParticipatingMarkets(acceptor, gameContext);

            var isIntersectingProducts = requesterMarkets.Intersect(acceptorMarkets).Count() > 0;

            return isIntersectingMarkets || isIntersectingProducts;
        }


        public static NicheType[] GetParticipatingMarkets(GameEntity company, GameContext gameContext)
        {
            if (company.hasProduct)
                return new NicheType[1] { company.product.Niche };

            var daughters = GetDaughterCompanies(gameContext, company.company.Id);

            return daughters.Where(d => d.hasProduct).Select(d => d.product.Niche).ToArray();
        }
    }
}

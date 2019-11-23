using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static void SendStrategicPartnershipRequest(GameEntity requester, GameEntity acceptor, bool notifyPlayer = false)
        {
            if (!requester.isIndependentCompany ||
                !acceptor.isIndependentCompany)
                return;

            // have partnership already
            if (requester.partnerships.Companies.Contains(acceptor.company.Id) ||
                acceptor.partnerships.Companies.Contains(requester.company.Id))
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
    }
}

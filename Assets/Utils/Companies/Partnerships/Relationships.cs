using System.Linq;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsCompetingCompany(int companyId1, int companyId2, GameContext gameContext) => IsCompetingCompany(Get(gameContext, companyId1), Get(gameContext, companyId2), gameContext);
        public static bool IsCompetingCompany(GameEntity company1, GameEntity company2, GameContext gameContext)
        {
            // you cannot compete with yourself:)
            if (company1.company.Id == company2.company.Id)
                return false;

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

        public static bool IsHaveStrategicPartnershipAlready(int c1, int c2, GameContext gameContext) => IsHaveStrategicPartnershipAlready(Get(gameContext, c1), Get(gameContext, c2));
        public static bool IsHaveStrategicPartnershipAlready(GameEntity c1, GameEntity c2)
        {
            return
                c1.partnerships.companies.Contains(c2.company.Id) &&
                c2.partnerships.companies.Contains(c1.company.Id);
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
    }
}

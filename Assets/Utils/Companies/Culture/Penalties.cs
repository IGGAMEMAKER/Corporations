namespace Assets.Utils
{
    partial class Companies
    {
        public static int GetPolicyValue(GameEntity company, CorporatePolicy policy) => GetOwnCorporateCulture(company)[policy];

        // policy Responsibility: LeaderOrTeam
        public static int GetCompanyLimit(GameEntity company)
        {
            var rank = 1;

            if (company.company.CompanyType == CompanyType.Corporation)
                rank = 3;

            if (company.company.CompanyType == CompanyType.Holding)
                rank = 2;

            return GetPolicyValue(company, CorporatePolicy.LeaderOrTeam) * rank;
        }

        public static int GetCompanyLimitPenalty(GameEntity company, GameContext gameContext)
        {
            var daughters = GetDaughterCompanies(gameContext, company);

            var daughtersCount = daughters.Length;
            var limit = GetCompanyLimit(company);

            if (daughtersCount > limit)
                return (daughtersCount - limit) * 30;

            return 0;
        }


        // policy Focusing: amount of primary markets
        public static int GetPrimaryMarketsLimit(GameEntity company, GameContext gameContext)
        {
            var rank = 1;

            if (company.company.CompanyType == CompanyType.Corporation)
                rank = 2;

            return GetPolicyValue(company, CorporatePolicy.Focusing) * rank;
        }

        public static int GetPrimaryMarketsAmount(GameEntity company)
        {
            return company.companyFocus.Niches.Count;
        }

        public static int GetPrimaryMarketsInnovationPenalty(GameEntity company, GameContext gameContext)
        {
            var amount = GetPrimaryMarketsAmount(company);

            var limit = GetPrimaryMarketsLimit(company, gameContext);

            if (amount > limit)
                return (amount - limit) * 5;

            return 0;
        }

        // policy Buy or Create

    }
}

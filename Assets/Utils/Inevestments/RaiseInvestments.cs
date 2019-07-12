namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static BonusContainer GetInvestorOpinionBonus(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            var description = new BonusContainer("Investor opinion");

            if (company.hasProduct)
                AppendProductBonuses(description, company, gameContext);
            else
                AppendCompanyGroupBonuses(description, company);

            description.Append("Same goals", 25);

            return description;
        }

        internal static string GetInvestorOpinionDescription(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            var description = GetInvestorOpinionBonus(gameContext, company, investor);

            string title = Visuals.Describe(
                description.Sum(),
                "They will invest in this company if asked",
                "They will not invest"
            );

            return title;
        }

        private static void AppendCompanyGroupBonuses(BonusContainer bonusContainer, GameEntity company)
        {
        }

        private static void AppendProductBonuses(BonusContainer bonusContainer, GameEntity company, GameContext gameContext)
        {
            bonusContainer.Append("Product competitiveness", NicheUtils.GetProductCompetitiveness(company, gameContext));
        }

        public static long GetInvestorOpinion(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            long opinion = 0;

            bool isSuitableByGoal = IsInvestorSuitableByGoal(investor.shareholder.InvestorType, company.companyGoal.InvestorGoal);

            var goalComparison = isSuitableByGoal ? 25 : -1000;
            opinion += goalComparison;

            if (company.hasProduct)
                opinion += GetProductCompanyOpinion(company, gameContext);

            return opinion;
        }

        public static bool IsInvestorSuitable(GameEntity shareholder, GameEntity company)
        {
            bool isSuitableByGoal = IsInvestorSuitableByGoal(shareholder.shareholder.InvestorType, company.companyGoal.InvestorGoal);
            bool isSuitableByNiche = IsInInvestorsSphereOfInfluence(shareholder, company);
            bool isSuitableByCompanySize = IsInvestorSuitableByCompanySize(shareholder, company.companyGoal.InvestorGoal);
            bool isInvestorAlready = IsInvestsInCompany(shareholder, company);

            return isSuitableByGoal && isSuitableByNiche && isSuitableByCompanySize && !isInvestorAlready;
        }

        public static bool IsInInvestorsSphereOfInfluence(GameEntity shareholder, GameEntity company)
        {
            if (!company.hasProduct)
                return true;

            return shareholder.companyFocus.Niches.Contains(company.product.Niche);
        }

        public static bool IsInvestsInCompany(GameEntity shareholder, GameEntity company)
        {
            return IsInvestsInCompany(shareholder.shareholder.Id, company);
        }

        public static bool IsInvestsInCompany(int shareholderId, GameEntity company)
        {
            return company.shareholders.Shareholders.ContainsKey(shareholderId);
        }

        public static bool IsInvestorSuitableByCompanySize(GameEntity shareholder, InvestorGoal goal)
        {
            return true;
        }

        public static bool IsInvestorSuitableByGoal(InvestorType shareholderType, InvestorGoal goal)
        {
            switch (goal)
            {
                case InvestorGoal.BecomeMarketFit:
                    return shareholderType == InvestorType.Angel;

                case InvestorGoal.BecomeProfitable:
                    return shareholderType == InvestorType.VentureInvestor;

                case InvestorGoal.GrowCompanyCost:
                    return shareholderType == InvestorType.VentureInvestor || shareholderType == InvestorType.Strategic;

                case InvestorGoal.IPO:
                    return shareholderType == InvestorType.Strategic || shareholderType == InvestorType.VentureInvestor;

                case InvestorGoal.Prototype:
                case InvestorGoal.FirstUsers:
                default:
                    return shareholderType == InvestorType.FFF;
            }
        }

        public static long GetProductCompanyOpinion(GameEntity company, GameContext gameContext)
        {
            var marketSituation = NicheUtils.GetProductCompetitiveness(company, gameContext);

            return marketSituation;
        }
    }
}

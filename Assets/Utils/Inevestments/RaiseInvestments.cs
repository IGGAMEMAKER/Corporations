namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static Bonus<float> GetInvestorOpinionBonus(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            var description = new Bonus<float>("Investor opinion");

            if (company.hasProduct)
                AppendProductBonuses(description, company, gameContext);

            if (IsInvestsInCompany(investor, company))
                description.AppendAndHideIfZero("Invests already", 35);

            description.Append("Same goals", 25);

            return description;
        }

        internal static string GetInvestorOpinionDescription(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            var description = GetInvestorOpinionBonus(gameContext, company, investor);

            string title = Visuals.DescribeValueWithText(
                (long)description.Sum(),
                "They will invest in this company if asked",
                "They will not invest",
                "They almost want to invest"
            );

            return title;
        }

        private static void AppendProductBonuses(Bonus<float> bonusContainer, GameEntity company, GameContext gameContext)
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
            bool isSuitableByNiche = IsInInvestorsSphereOfInfluence(shareholder, company);

            bool isInvestorAlready = IsInvestsInCompany(shareholder, company);

            bool isParentOfDependentCompany = !company.isIndependentCompany && isInvestorAlready;

            // prevent group investments
            // because of recursive investment bug
            bool isFinancialStructure = !shareholder.isManagingCompany;

            return
                isFinancialStructure
                && isSuitableByNiche
                && (isParentOfDependentCompany || company.isIndependentCompany);
        }

        public static bool IsInInvestorsSphereOfInfluence(GameEntity shareholder, GameEntity company)
        {
            if (!company.hasProduct)
                return NicheUtils.IsShareSameInterests(company, shareholder);

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

        public static int GetRandomInvestmentFund(GameContext context)
        {
            var funds = CompanyUtils.GetInvestmentFunds(context);

            var index = UnityEngine.Random.Range(0, funds.Length);

            return funds[index].shareholder.Id;
        }
    }
}

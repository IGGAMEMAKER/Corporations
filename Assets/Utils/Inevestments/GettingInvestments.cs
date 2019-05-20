using System;

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static bool IsInvestorSuitable(GameEntity shareholder, GameEntity company)
        {
            bool isSuitableByGoal = IsInvestorSuitableByGoal(shareholder.shareholder.InvestorType, company.companyGoal.InvestorGoal);
            bool isSuitableByNiche = true;
            bool isSuitableByCompanySize = IsInvestorSuitableByCompanySize(shareholder, company.companyGoal.InvestorGoal);
            bool isInvestorAlready = IsInvestsInThisCompany(shareholder, company);

            return isSuitableByGoal && isSuitableByNiche && isSuitableByCompanySize && !isInvestorAlready;
        }

        private static bool IsInvestsInThisCompany(GameEntity shareholder, GameEntity company)
        {
            return company.shareholders.Shareholders.ContainsKey(shareholder.shareholder.Id);
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
                    return shareholderType == InvestorType.VentureInvestor;

                case InvestorGoal.IPO:
                    return shareholderType == InvestorType.Strategic || shareholderType == InvestorType.VentureInvestor;

                default:
                    return shareholderType == InvestorType.FFF;
            }
        }

        public static long GetProductCompanyOpinion(GameEntity company, GameContext gameContext)
        {
            var marketSituation = NicheUtils.GetProductCompetitiveness(company, gameContext);

            return marketSituation;
        }

        public static long GetInvestorOpinion(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            long opinion = 0;

            var goalComparison = IsInvestorSuitableByGoal(investor.shareholder.InvestorType, company.companyGoal.InvestorGoal) ? 25 : -1000;
            opinion += goalComparison;

            if (company.hasProduct)
                opinion += GetProductCompanyOpinion(company, gameContext);

            return opinion;
        }
    }
}

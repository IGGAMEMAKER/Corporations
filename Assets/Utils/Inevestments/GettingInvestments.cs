using System;

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static bool IsInvestorSuitable(GameEntity shareholder, GameEntity company)
        {
            bool isInvestorAlready = IsInvestsInThisCompany(shareholder, company);
            bool isSuitableByGoal = IsInvestorSuitableByGoal(shareholder.shareholder.InvestorType, company.companyGoal.InvestorGoal);
            bool isInvestorSuitableByNiche = true;
            bool isInvestorSuitableByCompanySize = IsInvestorSuitableByCompanySize(shareholder, company.companyGoal.InvestorGoal);

            return isSuitableByGoal && isInvestorSuitableByNiche && isInvestorSuitableByCompanySize && !isInvestorAlready;
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

        public static int GetProductCompanyOpinion(GameEntity company)
        {
            int marketSituation = NicheUtils.GetProductCompetitivenessBonus(company);

            return marketSituation;
        }

        public static int GetInvestorOpinion(GameContext gameContext, GameEntity company, GameEntity investor)
        {
            int opinion = 0;

            int goalComparison = IsInvestorSuitableByGoal(investor.shareholder.InvestorType, company.companyGoal.InvestorGoal) ? 25 : -1000;
            opinion += goalComparison;

            if (company.hasProduct)
                opinion += GetProductCompanyOpinion(company);

            return opinion;
        }
    }
}

namespace Assets.Core
{
    partial class Companies
    {
        public static bool IsInvestmentRoundStarted(GameEntity company)
        {
            return company.hasAcceptsInvestments;
        }

        public static bool IsReadyToStartInvestmentRound(GameEntity company)
        {
            return !company.hasAcceptsInvestments;
        }

        public static void StartInvestmentRound(GameContext gameContext, GameEntity company)
        {
            if (IsReadyToStartInvestmentRound(company))
            {
                SpawnProposals(gameContext, company);

                //NotifyAboutInvestmentRound(company, gameContext);

                company.ReplaceInvestmentRounds(GetInvestmentRoundName(company));

                company.AddAcceptsInvestments(C.INVESTMENT_ROUND_ACTIVE_FOR_DAYS);
            }
        }



        public static InvestmentRound GetInvestmentRoundName(GameEntity company)
        {
            switch (company.companyGoal.InvestorGoal)
            {
                case InvestorGoalType.Prototype:
                    return InvestmentRound.Preseed;

                case InvestorGoalType.FirstUsers:
                    return InvestmentRound.Seed;

                case InvestorGoalType.BecomeMarketFit:
                    return InvestmentRound.A;

                case InvestorGoalType.Release:
                    return InvestmentRound.B;

                case InvestorGoalType.BecomeProfitable:
                    return InvestmentRound.C;

                case InvestorGoalType.GrowCompanyCost:
                    return InvestmentRound.C;

                case InvestorGoalType.IPO:
                    return InvestmentRound.D;

                default:
                    return InvestmentRound.E;
            }
        }

        public static void NotifyAboutInvestmentRound(GameEntity company, GameContext gameContext)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (IsInSphereOfInterest(playerCompany, company))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageInvestmentRoundStarted(company.company.Id));
        }
    }
}

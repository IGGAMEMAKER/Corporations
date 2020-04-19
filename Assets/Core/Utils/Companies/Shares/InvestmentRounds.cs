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

        public static void StartInvestmentRound(GameContext gameContext, int companyId) => StartInvestmentRound(gameContext, Get(gameContext, companyId));
        public static void StartInvestmentRound(GameContext gameContext, GameEntity company)
        {
            if (IsReadyToStartInvestmentRound(company))
            {
                SpawnProposals(gameContext, company.company.Id);

                NotifyAboutInvestmentRound(company, gameContext);

                company.ReplaceInvestmentRounds(GetInvestmentRoundName(company));

                company.AddAcceptsInvestments(C.INVESTMENT_ROUND_ACTIVE_FOR_DAYS);
            }
        }



        public static InvestmentRound GetInvestmentRoundName(GameEntity company)
        {
            switch (company.companyGoal.InvestorGoal)
            {
                case InvestorGoal.Prototype:
                    return InvestmentRound.Preseed;

                case InvestorGoal.FirstUsers:
                    return InvestmentRound.Seed;

                case InvestorGoal.BecomeMarketFit:
                    return InvestmentRound.A;

                case InvestorGoal.Release:
                    return InvestmentRound.B;

                case InvestorGoal.BecomeProfitable:
                    return InvestmentRound.C;

                case InvestorGoal.GrowCompanyCost:
                    return InvestmentRound.C;

                case InvestorGoal.IPO:
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

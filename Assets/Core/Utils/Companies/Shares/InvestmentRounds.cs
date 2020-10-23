using System.Linq;

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

        private static bool HasGoal(GameEntity company, InvestorGoalType goalType)
        {
            return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType);
        }

        public static InvestmentRound GetInvestmentRoundName(GameEntity company)
        {
            if (HasGoal(company, InvestorGoalType.Prototype))
                return InvestmentRound.Preseed;

            if (HasGoal(company, InvestorGoalType.FirstUsers))
                return InvestmentRound.Seed;

            if (HasGoal(company, InvestorGoalType.BecomeMarketFit))
                return InvestmentRound.A;

            if (HasGoal(company, InvestorGoalType.Release))
                return InvestmentRound.B;

            if (HasGoal(company, InvestorGoalType.BecomeProfitable))
                return InvestmentRound.C;

            if (HasGoal(company, InvestorGoalType.GrowCompanyCost))
                return InvestmentRound.C;

            if (HasGoal(company, InvestorGoalType.IPO))
                return InvestmentRound.D;

            return InvestmentRound.E;
        }

        public static void NotifyAboutInvestmentRound(GameEntity company, GameContext gameContext)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (IsInSphereOfInterest(playerCompany, company))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageInvestmentRoundStarted(company.company.Id));
        }
    }
}

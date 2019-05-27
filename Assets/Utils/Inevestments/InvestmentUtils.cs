using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        public static GameEntity GenerateAngel(GameContext gameContext)
        {
            var human = HumanUtils.GenerateHuman(gameContext);

            var investorId = GenerateInvestorId(gameContext);

            BecomeInvestor(gameContext, human, 1000000);

            TurnToAngel(gameContext, investorId);

            return human;
        }

        public static void TurnToAngel(GameContext gameContext, int investorId)
        {
            var investor = GetInvestorById(gameContext, investorId);

            investor.ReplaceShareholder(investor.shareholder.Id, investor.shareholder.Name, InvestorType.Angel);
        }

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            var investor = GetInvestorById(context, investorId);

            var companyResource = investor.companyResource;
            companyResource.Resources.AddMoney(sum);

            investor.ReplaceCompanyResource(companyResource.Resources);
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == investorId);
        }

        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return GetInvestorById(context, investorId).company.Id;
        }

        public static string GetInvestorGoalDescription(BlockOfShares shares)
        {
            return GetFormattedInvestorGoal(shares.InvestorGoal);
        }

        public static string GetInvestorGoal(BlockOfShares shares)
        {
            return GetFormattedInvestorGoal(shares.InvestorGoal);
        }
    }
}

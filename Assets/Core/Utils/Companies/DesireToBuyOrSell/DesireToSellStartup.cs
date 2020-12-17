using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        public static long GetDesireToSellStartupByInvestorType(GameEntity startup, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                case InvestorType.FFF:
                case InvestorType.VentureInvestor:
                    return OnGoalCompletion(startup, investorType);

                case InvestorType.StockExchange:
                    return GetStockExhangeTradeDesire(startup, shareholderId);

                case InvestorType.Founder:
                    return GetFounderExitDesire(startup, shareholderId, gameContext);

                case InvestorType.Strategic:
                    return GetStrategicInvestorExitDesire(startup, shareholderId, gameContext);

                default:
                    Debug.Log("GetDesireToSellStartupByInvestorType. Unknown investor type " + investorType.ToString());
                    return C.COMPANY_DESIRE_TO_SELL_NO;
            }
        }

        public static Ambition GetFounderAmbition(GameEntity company, GameContext gameContext)
        {
            var CEOId = company.cEO.HumanId;

            return Humans.GetAmbition(gameContext, CEOId);
        }

        public static long GetFounderExitDesire(GameEntity startup, int shareholderId, GameContext gameContext)
        {
            var founder = Investments.GetInvestor(gameContext, shareholderId);

            var ambition = Humans.GetFounderAmbition(founder.humanSkills.Traits, Humans.GetRating(founder));

            if (ambition == Ambition.EarnMoney)
                return C.COMPANY_DESIRE_TO_SELL_YES;

            return C.COMPANY_DESIRE_TO_SELL_NO;
        }

        public static long GetStrategicInvestorExitDesire(GameEntity startup, int shareholderId, GameContext context)
        {
            var managingCompany = GetInvestorById(context, shareholderId);

            bool interestedIn = IsInSphereOfInterest(managingCompany, startup);

            return interestedIn ? C.COMPANY_DESIRE_TO_SELL_NO : C.COMPANY_DESIRE_TO_SELL_YES;
        }

        public static long GetStockExhangeTradeDesire(GameEntity startup, int shareholderId)
        {
            return C.COMPANY_DESIRE_TO_SELL_YES;
        }


        public static long OnGoalCompletion(GameEntity startup, InvestorType investorType)
        {
            var goal = startup.companyGoal.Goals.LastOrDefault();
            bool goalCompleted = false; // !Investments.IsInvestorSuitableByGoal(investorType, startup.companyGoal.InvestorGoal);

            return goalCompleted ? C.COMPANY_DESIRE_TO_SELL_YES : C.COMPANY_DESIRE_TO_SELL_NO;
        }
    }
}

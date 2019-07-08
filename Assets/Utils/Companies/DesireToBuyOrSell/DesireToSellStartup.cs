using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // TODO move ambition function somewhere else
        public static Ambition GetFounderAmbition(int ambitions)
        {
            if (ambitions < 70)
                return Ambition.EarnMoney;

            if (ambitions < 75)
                return Ambition.RuleProduct;

            if (ambitions < 80)
                return Ambition.IPO;

            if (ambitions < 85)
                return Ambition.CreateUnicorn;

            return Ambition.RuleCorporation;
        }

        public static long GetDesireToSellStartup(GameEntity startup, GameContext gameContext)
        {
            var shareholders = startup.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                desireToSell += GetDesireToSellStartupByInvestorType(startup, block.InvestorType, invId, gameContext) * block.amount;
                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }

        public static long GetDesireToSellStartupByInvestorType(GameEntity startup, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            switch (investorType)
            {
                case InvestorType.Angel:
                    return GetAngelExitDesire(startup, shareholderId);

                case InvestorType.FFF:
                    return GetFFFExitDesire(startup, shareholderId);

                case InvestorType.StockExchange:
                    return GetStockExhangeTradeDesire(startup, shareholderId);

                case InvestorType.VentureInvestor:
                    return GetVentureInvestorExitDesire(startup, shareholderId);

                case InvestorType.Founder:
                    return GetFounderExitDesire(startup, shareholderId, gameContext);

                case InvestorType.Strategic:
                    return GetStrategicInvestorExitDesire(startup, shareholderId, gameContext);

                default:
                    return 0;
            }
        }

        public static long GetFounderExitDesire(GameEntity startup, int shareholderId, GameContext gameContext)
        {
            var founder = InvestmentUtils.GetInvestorById(gameContext, shareholderId);

            var ambitions = founder.humanSkills.Traits[TraitType.Ambitions];

            var ambition = GetFounderAmbition(ambitions);

            if (ambition == Ambition.EarnMoney)
                return 1;

            return 0;
        }

        public static long GetStrategicInvestorExitDesire(GameEntity startup, int shareholderId, GameContext context)
        {
            var managingCompany = GetInvestorById(context, shareholderId);

            bool interestedIn = IsInSphereOfInterest(managingCompany, startup);

            return interestedIn ? 0 : 1;
        }

        public static long GetStockExhangeTradeDesire(GameEntity startup, int shareholderId)
        {
            return 1;
        }

        public static long GetFFFExitDesire(GameEntity startup, int shareholderId)
        {
            bool goalCompleted = !InvestmentUtils.IsInvestorSuitableByGoal(InvestorType.Angel, startup.companyGoal.InvestorGoal);

            return goalCompleted ? 1 : 0;
        }

        public static long GetAngelExitDesire(GameEntity startup, int shareholderId)
        {
            bool goalCompleted = !InvestmentUtils.IsInvestorSuitableByGoal(InvestorType.Angel, startup.companyGoal.InvestorGoal);

            return goalCompleted ? 1 : 0;
        }

        public static long GetVentureInvestorExitDesire(GameEntity startup, int shareholderId)
        {
            bool goalCompleted = !InvestmentUtils.IsInvestorSuitableByGoal(InvestorType.Angel, startup.companyGoal.InvestorGoal);

            return goalCompleted ? 1 : 0;
        }
    }
}

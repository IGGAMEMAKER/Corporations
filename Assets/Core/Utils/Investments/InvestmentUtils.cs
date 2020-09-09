using Entitas;
using System;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        public static GameEntity[] GetOwnings(GameContext context, int shareholderId) => GetOwnings(context, Companies.GetInvestorById(context, shareholderId));
        public static GameEntity[] GetOwnings(GameContext context, GameEntity c) // c can be human, investor or company
        {
            if (!c.hasOwnings)
                return new GameEntity[0];

            return c.ownings.Holdings.Select(companyId => Companies.Get(context, companyId)).ToArray();
        }

        public static GameEntity GenerateAngel(GameContext gameContext)
        {
            var human = Humans.GenerateHuman(gameContext);

            var investorId = GenerateInvestorId(gameContext);

            BecomeInvestor(gameContext, human, 1000000);

            //TurnToAngel(gameContext, investorId);
            var investor = GetInvestor(gameContext, investorId);

            investor.ReplaceShareholder(investor.shareholder.Id, investor.shareholder.Name, InvestorType.Angel);

            return human;
        }

        //public static void TurnToAngel(GameContext gameContext, int investorId)
        //{
        //    var investor = GetInvestorById(gameContext, investorId);

        //    investor.ReplaceShareholder(investor.shareholder.Id, investor.shareholder.Name, InvestorType.Angel);
        //}

        public static void AddMoneyToInvestor(GameContext context, int investorId, long sum)
        {
            var investor = GetInvestor(context, investorId);

            Companies.AddResources(investor, sum);
        }

        public static GameEntity GetInvestor(GameContext context, int investorId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == investorId);
        }

        public static GameEntity GetCompanyByInvestorId(GameContext context, int investorId)
        {
            return GetInvestor(context, investorId);
        }

        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return GetCompanyByInvestorId(context, investorId).company.Id;
        }

        public static long GetInvestorCapitalCost(GameContext gameContext, GameEntity human)
        {
            var holdings = Companies.GetPersonalHoldings(gameContext, human.shareholder.Id, false);

            return Economy.GetHoldingCost(gameContext, holdings);
        }
    }
}

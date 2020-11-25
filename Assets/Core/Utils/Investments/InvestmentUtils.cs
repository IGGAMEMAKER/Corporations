using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        // entity can be company, investor or human?
        public static List<CompanyHolding> GetHoldings(GameContext context, GameEntity entity, bool recursively)
        {
            List<CompanyHolding> holdings = new List<CompanyHolding>();

            var ownings = GetOwnings(context, entity);

            foreach (var company in ownings)
            {
                var holding = new CompanyHolding
                {
                    //companyId = company.company.Id,
                    company = company,

                    control = Companies.GetShareSize(context, company, entity),

                    holdings = recursively ? GetHoldings(context, company, recursively) : new List<CompanyHolding>()
                };

                holdings.Add(holding);
            }

            return holdings;
        }

        public static GameEntity[] GetOwnings(GameContext context, GameEntity c) // c can be human, investor or company
        {
            if (!c.hasOwnings)
                return new GameEntity[0];

            return c.ownings.Holdings.Select(companyId => Companies.Get(context, companyId)).ToArray();
        }


        public static GameEntity GetInvestor(GameContext context, int investorId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == investorId);
        }

        public static GameEntity GetCompanyByInvestorId(GameContext context, int investorId)
        {
            return GetInvestor(context, investorId);
        }

        public static GameEntity GenerateAngel(GameContext gameContext)
        {
            var human = Humans.GenerateHuman(gameContext);

            var investorId = GenerateInvestorId(gameContext);

            BecomeInvestor(gameContext, human, 1000000);

            TurnToAngel(gameContext, investorId);

            return human;
        }

        public static void TurnToAngel(GameContext gameContext, int investorId)
        {
            var investor = GetInvestor(gameContext, investorId);

            investor.ReplaceShareholder(investor.shareholder.Id, investor.shareholder.Name, InvestorType.Angel);
        }

        public static long GetInvestorCapitalCost(GameContext gameContext, GameEntity human)
        {
            var holdings = Investments.GetHoldings(gameContext, human, false);

            return Economy.GetHoldingsCost(gameContext, holdings);
        }

        // --------------------------------------------------------------

        public static void SetVotingStyle(GameEntity company, VotingStyle votingStyle)
        {
            company.investmentStrategy.VotingStyle = votingStyle;
        }

        public static void SetGrowthStyle(GameEntity company, CompanyGrowthStyle growthStyle)
        {
            company.investmentStrategy.GrowthStyle = growthStyle;
        }

        public static void SetExitStrategy(GameEntity company, InvestorInterest interest)
        {
            company.investmentStrategy.InvestorInterest = interest;
        }
    }
}

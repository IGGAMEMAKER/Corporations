using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<GoalRequirements> GetGoalRequirements(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            // products
            if (goal is InvestmentGoalMakePrototype)
                return GetInvestmentGoalMakePrototypeReqs(goal, company, gameContext);

            if (goal is InvestmentGoalMakeProductMarketFit)
                return GetInvestmentGoalMakeProductMarketFitReqs(goal, company, gameContext);

            if (goal is InvestmentGoalPrepareForRelease)
                return GetInvestmentGoalPrepareForReleaseReqs(goal, company, gameContext);

            if (goal is InvestmentGoalRelease)
                return GetInvestmentGoalReleaseReqs(goal, company, gameContext);

            if (goal is InvestmentGoalFirstUsers)
                return GetInvestmentGoalFirstUsersReqs(goal, company, gameContext);

            if (goal is InvestmentGoalMillionUsers)
                return GetInvestmentGoalMillionUsersReqs(goal, company, gameContext);

            if (goal is InvestmentGoalMoreSegments)
                return GetWrappedGoalList(new GoalRequirements { description = "Get more segments", have = 1, need = 1 });

            if (goal is InvestmentGoalGrowAudience)
                return GetInvestmentGoalGrowAudienceReqs(goal, company, gameContext);

            // all

            if (goal is InvestmentGoalBecomeProfitable)
                return GetInvestmentGoalBecomeProfitableReqs(goal, company, gameContext);

            if (goal is InvestmentGoalGrowProfit)
                return GetInvestmentGoalGrowProfitReqs(goal, company, gameContext);

            if (goal is InvestmentGoalRegainLoyalty)
                return GetWrappedGoalList(new GoalRequirements { have = 1, need = 1, description = "Regain loyalty" });

            if (goal is InvestmentGoalStartMonetisation)
                return GetInvestmentGoalStartMonetisationReqs(goal, company, gameContext);

            if (goal is InvestmentGoalGrowCost)
                return GetInvestmentGoalGrowCostReqs(goal, company, gameContext);

            if (goal is InvestmentGoalOutcompeteByIncome)
                return GetInvestmentGoalOutcompeteByIncomeReqs(goal, company, gameContext);


            if (goal is InvestmentGoalOutcompeteByUsers)
                return GetInvestmentGoalOutcompeteByUsersReqs(goal, company, gameContext);


            if (goal is InvestmentGoalOutcompeteByCost)
                return GetInvestmentGoalOutcompeteByCostReqs(goal, company, gameContext);

            if (goal is InvestmentGoalBuyBack)
                return GetInvestmentGoalBuyBackReqs(goal, company, gameContext);

            if (goal is InvestmentGoalDominateMarket)
                return GetInvestmentGoalDominateMarketReqs(goal, company, gameContext);

            if (goal is InvestmentGoalAcquireCompany)
                return GetInvestmentGoalAcquireCompanyReqs(goal, company, gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = 14,
                        need = 44,

                        description = $"Unknown GOAL ({goal.InvestorGoalType})"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalMakePrototypeReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            GameEntity product = GetProduct(goal, company, gameContext);
            var loyalty = 5;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = (long)Marketing.GetChurnRate(product, gameContext),
                        need = loyalty,
                        reversedCheck = true,

                        description = $"Churn < {loyalty}"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalMakeProductMarketFitReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            GameEntity product = GetProduct(goal, company, gameContext);

            var loyalty = 2;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = (long)Marketing.GetChurnRate(product, gameContext),
                        need = loyalty,
                        reversedCheck = true,

                        description = $"Churn < {loyalty}"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalPrepareForReleaseReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            GameEntity product = GetProduct(goal, company, gameContext);
            int teams = 5;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = product.team.Teams[0].Managers.Count,
                        need = teams,

                        description = $"Has at least {teams} workers in Core team"
                    },
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalReleaseReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            GameEntity product = GetProduct(goal, company, gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = product.isRelease ? 1 : 0,
                        need = 1,

                        description = "PRESS RELEASE BUTTON!"
                    },
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalFirstUsersReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalFirstUsers;
            GameEntity product = GetProduct(goal, company, gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Marketing.GetUsers(product),
                        need = g.TargetUsersAmount,

                        description = "Users > " + Format.Minify(g.TargetUsersAmount)
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalMillionUsersReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalMillionUsers;
            GameEntity product = GetProduct(goal, company, gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Marketing.GetUsers(product),
                        need = 1_000_000,

                        description = "Users > " + Format.Minify(g.TargetUsersAmount)
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalGrowAudienceReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalGrowAudience;

            GameEntity product = GetProduct(goal, company, gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Marketing.GetUsers(product),
                        need = g.TargetUsersAmount,

                        description = "Users > " + Format.Minify(g.TargetUsersAmount)
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalBecomeProfitableReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalBecomeProfitable;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.GetIncome(gameContext, company),
                        need = g.Income,

                        description = "Income > " + Format.Money(g.Income)
                    },

                    new GoalRequirements
                    {
                        have = Economy.IsProfitable(gameContext, company) ? 1 : 0,
                        need = 1,

                        description = "Profitable"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalGrowProfitReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalGrowProfit;
            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.GetIncome(gameContext, company),
                        need = g.Profit,

                        description = "Income > " + Format.Money(g.Profit)
                    },
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalStartMonetisationReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.GetIncome(gameContext, company),
                        need = 1,

                        description = "Income from product > 0"
                    },
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalGrowCostReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalGrowCost;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.CostOf(company, gameContext),
                        need = g.Cost,

                        description = "Cost > " + Format.Money(g.Cost)
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalOutcompeteByIncomeReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalOutcompeteByIncome;
            var competitor = Companies.Get(gameContext, g.CompanyId);

            var income = Economy.GetIncome(gameContext, competitor);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.GetIncome(gameContext, company),
                        need = income,

                        description = "Your income > income of " + g.CompetitorName + $"({Format.Money(income)})"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalOutcompeteByUsersReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalOutcompeteByUsers;

            var users = Marketing.GetUsers(Companies.Get(gameContext, g.CompanyId));

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Marketing.GetUsers(company),
                        need = users,

                        description = "Your audience > audience of " + g.CompetitorName + $"({Format.Minify(users)})"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalOutcompeteByCostReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalOutcompeteByCost;
            var cost = Economy.CostOf(Companies.Get(gameContext, g.CompanyId), gameContext);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Economy.CostOf(company, gameContext),
                        need = cost,

                        description = "Your cost > cost of " + g.CompetitorName + $"({Format.Minify(cost)})"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalAcquireCompanyReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalAcquireCompany;
            var target = Companies.Get(gameContext, g.CompanyId);

            var group = company.hasProduct ? Companies.GetManagingCompanyOf(company, gameContext) : company;

            bool companyWasBought = Companies.IsDaughterOf(group, target);
            bool companyBecomeBankrupt = !target.isAlive;

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = (companyWasBought ? 1 : 0) + (companyBecomeBankrupt ? 1 : 0),
                        need = 1,

                        description = "Buy " + g.CompetitorName
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalDominateMarketReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            var g = goal as InvestmentGoalDominateMarket;
            var companies = Markets.GetProductsOnMarket(gameContext, g.TargetMarket);

            var have = companies.Count(c => Companies.IsDaughterOf(company, c) && c.isAlive);
            var need = companies.Count(c => c.isAlive);

            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = have,
                        need = need,

                        description = "Own ALL companies in " + Enums.GetFormattedNicheName(g.TargetMarket) + $" market ({have}/{need})"
                    }
                };
        }

        private static List<GoalRequirements> GetInvestmentGoalBuyBackReqs(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            bool hasSoloShareholder = company.shareholders.Shareholders.Count == 1;
            return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = hasSoloShareholder ? 1 : 0,
                        need = 1,

                        description = "Buy all shares from your investors"
                    }
                };
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            return IsCompleted(goal, company, gameContext);
        }

        public static bool CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            //Companies.Log(company, "Try complete goal: " + goal.GetFormattedName());
            bool willComplete = forceComplete || CanCompleteGoal(company, gameContext, goal);

            if (willComplete)
            {
                Companies.LogSuccess(company, $"Completed goal: {goal.GetFormattedName()}");

                var executor = GetExecutor(goal, company, gameContext);

                CompleteGoal2(executor, goal);

                if (goal.NeedsReport)
                {
                    var controller = GetController(goal, company, gameContext);

                    CompleteGoal2(controller, goal);
                }
            }
            //else
            //{
            //    Companies.Log(company, "Not all requirements were met (\n\n" + goal.GetFormattedRequirements(company, gameContext));
            //}


            return willComplete;
        }

        public static void CompleteGoal2(GameEntity company, InvestmentGoal goal)
        {
            company.completedGoals.Goals.Add(goal.InvestorGoalType);

            company.companyGoal.Goals.Remove(goal);
        }

        public static bool IsCanCompleteAnyGoal(GameEntity company, GameContext gameContext)
        {
            return company.companyGoal.Goals.Any(g => CanCompleteGoal(company, gameContext, g));
        }

        public static void CompleteGoals(GameEntity company, GameContext gameContext)
        {
            var goals = company.companyGoal.Goals;

            for (var i = goals.Count - 1; i >= 0; i--)
            {
                var g = goals[i];

                CompleteGoal(company, gameContext, g);
            }
        }

        // Goal requirements
        public static GameEntity GetProduct(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return GetExecutor(goal, company, gameContext);
        }

        public static List<GoalRequirements> GetGoalRequirements(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            if (goal is InvestmentGoalMakePrototype)
            {
                GameEntity product = GetProduct(goal, company, gameContext);
                var loyalty = 10;

                return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = (long)Marketing.GetChurnRate(product, gameContext),
                        need = loyalty,

                        description = $"Churn < {loyalty}"
                    }
                };
            }

            if (goal is InvestmentGoalMakeProductMarketFit)
            {
                GameEntity product = GetProduct(goal, company, gameContext);

                var loyalty = 5;

                return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = (long)Marketing.GetChurnRate(product, gameContext),
                        need = loyalty,

                        description = $"Churn < {loyalty}"
                    }
                };
            }

            if (goal is InvestmentGoalPrepareForRelease)
            {
                GameEntity product = GetProduct(goal, company, gameContext);
                int teams = 3;

                return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = product.team.Teams.Count,
                        need = teams,

                        description = $"Has at least {teams} teams"
                    },
                };
            }

            if (goal is InvestmentGoalRelease)
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

            if (goal is InvestmentGoalFirstUsers)
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

            if (goal is InvestmentGoalMillionUsers)
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

            /*if (goal is InvestmentGoalMoreSegments)
            {
                var g = goal as InvestmentGoalMoreSegments;
                GameEntity product = GetProduct(goal, company, gameContext);

                return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = Marketing.GetAmountOfTargetAudiences(product),
                        need = g.Segments,

                        description = "Target audiences >= " + Format.Minify(g.Segments)
                    }
                };
            }*/

            if (goal is InvestmentGoalGrowAudience)
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

            if (goal is InvestmentGoalBecomeProfitable)
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

            if (goal is InvestmentGoalGrowProfit)
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

            /*if (goal is InvestmentGoalRegainLoyalty)
            {
                return new List<GoalRequirements>
                {

                    new GoalRequirements
                    {
                        have = Marketing.GetAudienceInfos().Any(a => Marketing.IsAudienceDisloyal(company, a.ID) && Marketing.IsTargetAudience(company, a.ID)) ? 0 : 1,
                        need = 1
                    }
                };
            }*/

            if (goal is InvestmentGoalStartMonetisation)
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

            if (goal is InvestmentGoalGrowCost)
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

            if (goal is InvestmentGoalOutcompeteByIncome)
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

            if (goal is InvestmentGoalOutcompeteByUsers)
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

            if (goal is InvestmentGoalOutcompeteByCost)
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

            if (goal is InvestmentGoalBuyBack)
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

            if (goal is InvestmentGoalDominateMarket)
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

            if (goal is InvestmentGoalAcquireCompany)
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

            //if (goal is InvestmentGoalUnknown)
            //{
                return new List<GoalRequirements>
                {
                    new GoalRequirements
                    {
                        have = 0,
                        need = 5,

                        description = $"Unknown requirements? ({goal.InvestorGoalType})"
                    }
                };
            //}
        }

        public static bool IsCompleted(InvestmentGoal goal, GameEntity company1, GameContext gameContext)
        {
            var company = GetExecutor(goal, company1, gameContext);

            var r = GetGoalRequirements(goal, company, gameContext);

            foreach (var req in r)
            {
                if (!IsRequirementMet(req, company, gameContext))
                    return false;
            }

            return true;
        }

        public static string GetFormattedRequirements(InvestmentGoal goal, GameEntity company1, GameContext gameContext)
        {
            var executor = GetExecutor(goal, company1, gameContext);

            return "as " + executor.company.Name + "\n" + string.Join("\n", GetGoalRequirements(goal, executor, gameContext)
                .Select(g => Visuals.Colorize($"<b>{g.description}</b>", IsRequirementMet(g, executor, gameContext))));
        }

        public static GameEntity GetExecutor(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return goal.ExecutorId == company.company.Id ? company : Companies.Get(gameContext, goal.ExecutorId);
        }

        public static GameEntity GetController(InvestmentGoal goal, GameEntity company, GameContext gameContext)
        {
            return goal.ControllerId == company.company.Id ? company : Companies.Get(gameContext, goal.ControllerId);
        }

        public static bool IsRequirementMet(GoalRequirements req, GameEntity company, GameContext gameContext)
        {
            return req.have >= req.need;
        }
    }
}

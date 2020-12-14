using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetNewGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestmentGoal>();

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            // competition ------------------------------

            //int index = 0;
            //var sortedCompetitors = Companies.GetSortedCompetitors(company, Q, ref index, false);

            var numeratedCompetitors = Companies.GetCompetitorsOf(company, Q, true).Select((c, i) => new { c, i, cost = Economy.CostOf(c, Q) });

            var nn = numeratedCompetitors.First(nc => nc.c.company.Id == company.company.Id);

            var strongerCompetitors = numeratedCompetitors.Where(nc1 => nc1.cost > nn.cost).Select(nc1 => nc1.c);
            var weakerCompetitors   = numeratedCompetitors.Where(nc1 => nc1.cost < nn.cost).Select(nc1 => nc1.c);

            var strongerDirectCompetitors = strongerCompetitors.Where(cc => Companies.IsDirectCompetitor(cc, nn.c));
            var weakerDirectCompetitors     = weakerCompetitors.Where(cc => Companies.IsDirectCompetitor(cc, nn.c));

            // ----------------------------------------



            // product only
            if (isProduct)
            {
                try
                {
                    goals.AddRange(GetWrappedProductGoals(company, Q));
                }
                catch
                {
                    Debug.LogError("GetNewGoals Error for product");
                }
            }

            // group only
            if (isGroup)
            {
                try
                {
                    // daughters need to be dependant on parent company
                    foreach (var daughter in Companies.GetDaughterProducts(Q, company))
                    {
                        goals.AddRange(GetWrappedProductGoals(daughter, Q, company));
                    }
                }
                catch
                {
                    Debug.LogError("Is group");
                }

                try
                {
                    goals.AddRange(GetWrappedGroupGoals(company, Q, strongerCompetitors, weakerCompetitors, strongerDirectCompetitors, weakerDirectCompetitors));
                }
                catch
                {
                    Debug.LogError("Tried to add group goals, but failed");
                }

            }

            // both
            try
            {
                goals.AddRange(GetWrappedCommonGoals(company, Q, strongerCompetitors, weakerCompetitors, strongerDirectCompetitors, weakerDirectCompetitors));
            }
            catch (Exception ex)
            {
                Debug.LogError("added common goals " + ex);
            }

            // only show buy back goal if there is only that goal
            if (goals.Any(g => g.InvestorGoalType == InvestorGoalType.BuyBack))
                return OnlyGoal(goals.First(g => g.InvestorGoalType == InvestorGoalType.BuyBack));

            if (company.companyGoal.Goals.Any(g => g.InvestorGoalType == InvestorGoalType.BuyBack))
                return new List<InvestmentGoal>();

            return goals;
        }

        private static List<InvestmentGoal> GetWrappedProductGoals(GameEntity company, GameContext Q, GameEntity controller = null)
        {
            return Wrap(GetProductGoals(company, Q), company, Q, controller);
        }

        private static List<InvestmentGoal> GetWrappedGroupGoals(GameEntity company, GameContext Q, IEnumerable<GameEntity> strongerCompetitors, IEnumerable<GameEntity> weakerCompetitors, IEnumerable<GameEntity> strongerDirectCompetitors, IEnumerable<GameEntity> weakerDirectCompetitors)
        {
            return Wrap(GetGroupOnlyGoals(company, Q, strongerCompetitors, weakerCompetitors, strongerDirectCompetitors, weakerDirectCompetitors), company, Q);
        }

        private static List<InvestmentGoal> GetWrappedCommonGoals(GameEntity company, GameContext Q, IEnumerable<GameEntity> strongerCompetitors, IEnumerable<GameEntity> weakerCompetitors, IEnumerable<GameEntity> strongerDirectCompetitors, IEnumerable<GameEntity> weakerDirectCompetitors)
        {
            return Wrap(GetCommonGoals(company, Q, strongerCompetitors, weakerCompetitors, strongerDirectCompetitors, weakerDirectCompetitors), company, Q);
        }

        public static bool HasGoalAlready(GameEntity company, GameContext gameContext, InvestorGoalType goalType)
        {
            return company.companyGoal.Goals.Any(g => g.InvestorGoalType == goalType);
        }

        public static bool Completed(GameEntity company1, InvestorGoalType goal) => IsGoalCompleted(company1, goal);
        public static bool IsGoalCompleted(GameEntity company, InvestorGoalType goal)
        {
            return company.completedGoals.Goals.Contains(goal);
        }

        public static void AddOnce(List<InvestmentGoal> goals, GameEntity company, InvestmentGoal goal)
        {
            if (!Completed(company, goal.InvestorGoalType))
                goals.Add(goal);
        }


        public static List<InvestmentGoal> OnlyGoal(InvestmentGoal goal) => new List<InvestmentGoal> { goal };
        //public static List<InvestorGoalType> OnlyGoal(InvestorGoalType goal) => new List<InvestorGoalType> { goal };
        public static List<InvestmentGoal> Wrap(List<InvestmentGoal> goals, GameEntity executor, GameContext Q, GameEntity controller = null)
        {
            return goals
                .Where(g => !HasGoalAlready(executor, Q, g.InvestorGoalType))
                
                .Select(g => g.SetExecutorAndController(executor, controller ?? executor))
                .ToList();
        }
        //public static List<InvestmentGoal> Wrap(List<InvestorGoalType> goals, GameEntity executor, GameContext Q, GameEntity controller = null)
        //{
        //    return goals
        //        .Where(g => !HasGoalAlready(executor, Q, g))

        //        .Select(g => GetInvestmentGoal(executor, Q, g).SetExecutorAndController(executor, controller ?? executor))
        //        .ToList();
        //}
    }
}

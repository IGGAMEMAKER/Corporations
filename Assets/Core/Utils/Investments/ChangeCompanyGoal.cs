using System.Collections.Generic;

public struct GoalRequirements
{
    public long have;
    public long need;
}

namespace Assets.Core
{
    public static partial class Investments
    {
        public static bool CanCompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal)
        {
            var r = GetGoalRequirements(company, gameContext, goal);

            foreach (var req in r)
            {
                if (req.have < req.need)
                    return false;
            }

            return true;
        }

        public static InvestorGoalType GetNextGoal(InvestorGoalType current)
        {
            switch (current)
            {
                case InvestorGoalType.Prototype: return InvestorGoalType.FirstUsers;
                case InvestorGoalType.FirstUsers: return InvestorGoalType.BecomeMarketFit;
                case InvestorGoalType.BecomeMarketFit: return InvestorGoalType.Release;
                case InvestorGoalType.Release: return InvestorGoalType.BecomeProfitable;
                case InvestorGoalType.BecomeProfitable: return InvestorGoalType.Operationing;

                default: return current;
            }
        }

        public static bool Done(GameEntity company, InvestorGoalType goal)
        {
            return company.completedGoals.Goals.Contains(goal);
        }

        public static bool IsPickableGoal(GameEntity company, GameContext gameContext, InvestorGoalType goal)
        {
            var users = Marketing.GetUsers(company);
            var cost = Economy.CostOf(company, gameContext);

            bool isProduct = company.hasProduct;
            bool isGroup = !isProduct;

            var income = Economy.GetIncome(gameContext, company);

            bool focusedProduct = isProduct && Marketing.IsFocusingOneAudience(company);

            var minLoyalty = 5;
            var marketFit = 10; // 10 cause it allows monetisation for ads

            List<InvestorGoalType> RedoableGoals = new List<InvestorGoalType> {
                InvestorGoalType.GrowIncome, InvestorGoalType.GrowUserBase, InvestorGoalType.GainMoreSegments,
                InvestorGoalType.OutcompeteCompanyByUsers, InvestorGoalType.OutcompeteCompanyByMarketShare, InvestorGoalType.OutcompeteCompanyByIncome,
                InvestorGoalType.AcquireCompany, InvestorGoalType.GrowCompanyCost
            };

            List<InvestorGoalType> OneTimeGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.Release, InvestorGoalType.BecomeMarketFit, InvestorGoalType.FirstUsers, InvestorGoalType.Prototype
            };

            // this goal was done already && cannot be done twice
            if (company.completedGoals.Goals.Contains(goal) && OneTimeGoals.Contains(goal))
                return false;

            if (company.completedGoals.Goals.Count == 0)
                return goal == InvestorGoalType.Prototype;

            bool isPrototype = isProduct && !company.isRelease && focusedProduct;
            bool releasedProduct = isProduct && company.isRelease;

            bool profitable = Economy.IsProfitable(gameContext, company);

            

            switch (goal)
            {
                // PRODUCTS

                //case InvestorGoalType.Prototype:
                //    return focusedProduct && isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < minLoyalty;

                case InvestorGoalType.BecomeMarketFit:
                    return isPrototype && Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)) < marketFit;

                case InvestorGoalType.FirstUsers:
                    return isPrototype && users < 2000;

                case InvestorGoalType.Release:
                    return isPrototype;

                case InvestorGoalType.GrowUserBase:
                    return releasedProduct && users > 50_000;

                case InvestorGoalType.GrowIncome:
                    return releasedProduct && profitable && income > 100_000;

                case InvestorGoalType.GainMoreSegments:
                    return releasedProduct && users > 1_000_000 && company.marketing.ClientList.Count < Marketing.GetAudienceInfos().Count;


                // GROUPS
                case InvestorGoalType.IPO:
                    return isGroup && !company.isPublicCompany && cost > C.IPO_REQUIREMENTS_COMPANY_COST;

                // BOTH
                case InvestorGoalType.GrowCompanyCost:
                    return (releasedProduct || isGroup) && profitable;

                case InvestorGoalType.BecomeProfitable:
                    return !profitable;

                case InvestorGoalType.None:
                    return false;

                default:
                    return isGroup;
            }
        }

        public static void CompleteGoal(GameEntity company, GameContext gameContext, InvestmentGoal goal, bool forceComplete = false)
        {
            if (forceComplete || CanCompleteGoal(company, gameContext, goal))
            {
                company.completedGoals.Goals.Add(goal.InvestorGoalType);
            }
        }

        public static void CompleteGoals(GameEntity company, GameContext gameContext)
        {
            var goals = company.companyGoal.Goals;

            for (var i = goals.Count - 1; i > 0; i--)
            {
                var g = goals[i];

                if (CanCompleteGoal(company, gameContext, g))
                {
                    company.completedGoals.Goals.Add(g.InvestorGoalType);
                    goals.RemoveAt(i);
                }
            }
        }

        internal static void AddCompanyGoal(GameEntity company, InvestmentGoal goal)
        {
            company.companyGoal.Goals.Add(goal);
        }
    }
}

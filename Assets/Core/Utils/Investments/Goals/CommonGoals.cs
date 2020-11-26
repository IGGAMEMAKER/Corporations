using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetCommonGoals(GameEntity company, GameContext Q, IEnumerable<GameEntity> strongerCompetitors, IEnumerable<GameEntity> weakerCompetitors, IEnumerable<GameEntity> strongerDirectCompetitors, IEnumerable<GameEntity> weakerDirectCompetitors)
        {
            var goals = new List<InvestmentGoal>();

            #region data
            bool releasedProduct = company.hasProduct && company.isRelease;

            bool isGroup = !company.hasProduct;

            var income = Economy.GetIncome(Q, company);
            bool profitable = Economy.IsProfitable(Q, company);

            bool solidCompany = (releasedProduct || isGroup) && income > 500_000;

            // weaker
            bool hasWeakerDirectCompetitors = weakerDirectCompetitors.Count() > 0;
            bool hasWeakerCompetitors = weakerCompetitors.Count() > 0;

            GameEntity weakerCompany = null;

            if (hasWeakerDirectCompetitors)
                weakerCompany = weakerDirectCompetitors.Last();
            else if (hasWeakerCompetitors)
                weakerCompany = weakerCompetitors.Last();

            // stronger
            bool hasStrongerDirectCompetitors = strongerDirectCompetitors.Count() > 0;
            bool hasStrongerCompetitors = strongerCompetitors.Count() > 0;

            GameEntity strongerCompany = null;

            if (hasStrongerDirectCompetitors)
                strongerCompany = strongerDirectCompetitors.Last();
            else if (hasStrongerCompetitors)
                strongerCompany = strongerCompetitors.Last();
            #endregion

            if (solidCompany)
            {
                goals.Add(new InvestmentGoalGrowCost(Economy.CostOf(company, Q) * 3 / 2));
                goals.Add(new InvestmentGoalGrowProfit(Economy.GetIncome(Q, company) * 3 / 2));
                //goals.Add(InvestorGoalType.GrowCompanyCost);
                //goals.Add(InvestorGoalType.GrowIncome);

                if (!profitable)
                {
                    goals.Add(new InvestmentGoalBecomeProfitable(Economy.GetIncome(Q, company)));
                    //goals.Add(InvestorGoalType.BecomeProfitable);
                }

                if (strongerCompany != null)
                {
                    goals.Add(new InvestmentGoalOutcompeteByIncome(strongerCompany));
                    goals.Add(new InvestmentGoalOutcompeteByCost(strongerCompany));
                    //goals.Add(InvestorGoalType.OutcompeteCompanyByIncome);
                    //goals.Add(InvestorGoalType.OutcompeteCompanyByCost);
                    ////goals.Add(InvestorGoalType.OutcompeteCompanyByUsers);
                }
            }

            return goals;
        }
    }
}

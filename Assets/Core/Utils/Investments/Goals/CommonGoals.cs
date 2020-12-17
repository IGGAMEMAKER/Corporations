using System.Collections.Generic;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetCommonGoals(GameEntity company, GameContext Q)
        {
            var goals = new List<InvestmentGoal>();

            #region data
            bool releasedProduct = company.hasProduct && company.isRelease;

            bool isGroup = !company.hasProduct;

            var income = Economy.GetIncome(Q, company);
            bool profitable = Economy.IsProfitable(Q, company);

            bool solidCompany = (releasedProduct || isGroup) && income > 100_000;

            // weaker
            GameEntity weakerCompany = Companies.GetWeakerCompetitor(company, Q, true); // null;

            // stronger
            GameEntity strongerCompany = Companies.GetStrongerCompetitor(company, Q, true); // null;

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
                    goals.Add(new InvestmentGoalOutcompeteByIncome(strongerCompany.company.Id, strongerCompany.company.Name));
                    //goals.Add(new InvestmentGoalOutcompeteByCost(strongerCompany.company.Id, strongerCompany.company.Name));


                    //goals.Add(InvestorGoalType.OutcompeteCompanyByIncome);
                    //goals.Add(InvestorGoalType.OutcompeteCompanyByCost);
                    ////goals.Add(InvestorGoalType.OutcompeteCompanyByUsers);
                }
            }

            return goals;
        }
    }
}

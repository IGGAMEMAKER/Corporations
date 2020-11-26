using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetGroupOnlyGoals(GameEntity company, GameContext Q, IEnumerable<GameEntity> strongerCompetitors, IEnumerable<GameEntity> weakerCompetitors, IEnumerable<GameEntity> strongerDirectCompetitors, IEnumerable<GameEntity> weakerDirectCompetitors)
        {
            var goals = new List<InvestmentGoal>();

            var groupGoals = new List<InvestorGoalType>
            {
                InvestorGoalType.AcquireCompany,
                InvestorGoalType.DominateSegment, // 50%+ users
                InvestorGoalType.DominateMarket, // OWN ALL COMPANIES
                InvestorGoalType.BuyBack,
                InvestorGoalType.IPO,
            };

            #region data
            var income = Economy.GetIncome(Q, company);

            bool solidCompany = income > 50_000;


            GameEntity weakerCompany; // null;
            #endregion

            var daughters = Companies.GetDaughterProducts(Q, company);

            if (solidCompany)
            {
                //goals.Add(InvestorGoalType.AcquireCompany);

                if (daughters.Count() > 0)
                {
                    var flagship = daughters.First();
                    var flagshipCompetitors = Companies.GetDirectCompetitors(flagship, Q, false);

                    if (flagshipCompetitors.Count() > 0)
                    {
                        weakerCompany = flagshipCompetitors.OrderByDescending(c => Economy.CostOf(c, Q)).Last();
                    }
                    else
                    {
                        weakerCompany = Companies.GetWeakerCompetitor(company, Q, true);
                    }

                    var acquisitionGoal = new InvestmentGoalAcquireCompany(weakerCompany.company.Id, weakerCompany.company.Name);

                    if (!Investments.CanCompleteGoal(company, Q, acquisitionGoal))
                        goals.Add(acquisitionGoal);
                }
            }


            #region DominateSegment
            //if (solidCompany && company.companyFocus.Niches.Count == 1)
            //{
            //    var first = daughters.First();

            //    var positioning = Marketing.GetPositioning(first);

            //    if (Companies.GetMarketShareOfCompanyMultipliedByHundred(company, Q) < 2)
            //    goals.Add(InvestorGoalType.DominateSegment);

            //}
            #endregion

            if (solidCompany && daughters.Count() > 2)
                AddOnce(goals, company, new InvestmentGoalDominateMarket(company.companyFocus.Niches.First()));
                //AddOnce(goals, company, InvestorGoalType.DominateMarket);

            if (Completed(company, InvestorGoalType.DominateMarket))
                goals.Add(new InvestmentGoalBuyBack());
                //goals.Add(InvestorGoalType.BuyBack);

            return goals;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Investments
    {
        public static List<InvestmentGoal> GetGroupOnlyGoals(GameEntity company, GameContext Q)
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
                if (daughters.Any())
                {
                    var flagship = daughters.First();
                    var flagshipCompetitors = Companies.GetDirectCompetitors(flagship, Q, false);

                    if (flagshipCompetitors.Any())
                    {
                        weakerCompany = flagshipCompetitors.OrderByDescending(c => Economy.CostOf(c, Q)).Last();
                    }
                    else
                    {
                        weakerCompany = Companies.GetWeakerCompetitor(company, Q, true);
                    }

                    // if there are weaker companies
                    if (weakerCompany != null)
                    {
                        var acquisitionGoal = new InvestmentGoalAcquireCompany(weakerCompany.company.Id, weakerCompany.company.Name);

                        if (!CanCompleteGoal(company, Q, acquisitionGoal))
                            goals.Add(acquisitionGoal);
                    }
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

            if (solidCompany && daughters.Length > 2)
                AddOnce(goals, company, new InvestmentGoalDominateMarket(company.companyFocus.Niches.First()));

            if (Completed(company, InvestorGoalType.DominateMarket))
                 return OnlyGoal(new InvestmentGoalBuyBack());

            return goals;
        }
    }
}

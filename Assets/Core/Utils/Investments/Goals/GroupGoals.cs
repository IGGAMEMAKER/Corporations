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


            bool hasWeakerDirectCompetitors = weakerDirectCompetitors.Count() > 0;
            bool hasWeakerCompetitors = weakerCompetitors.Count() > 0;

            bool isGroupOrGlobalProduct = true;

            GameEntity weakerCompany = null;

            if (hasWeakerDirectCompetitors)
                weakerCompany = weakerDirectCompetitors.Last();
            else if (hasWeakerCompetitors && isGroupOrGlobalProduct)
                weakerCompany = weakerCompetitors.Last();

            #endregion

            if (solidCompany && weakerCompany != null)
            {
                //goals.Add(InvestorGoalType.AcquireCompany);
                goals.Add(new InvestmentGoalAcquireCompany(weakerCompany));
            }

            var daughters = Companies.GetDaughterProducts(Q, company);

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
                AddOnce(goals, company, new InvestmentGoalDominateMarket(company.product.Niche));
                //AddOnce(goals, company, InvestorGoalType.DominateMarket);

            if (Completed(company, InvestorGoalType.DominateMarket))
                goals.Add(new InvestmentGoalBuyBack());
                //goals.Add(InvestorGoalType.BuyBack);

            return goals;
        }
    }
}

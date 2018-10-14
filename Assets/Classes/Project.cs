using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    class Project
    {
        List<Feature> Features;
        TeamResource Resource;

        ShareholderInfo Shareholders;
        Audience audience;
        Team team;

        public Project(int featureCount, Audience audience, Team team, TeamResource resource, ShareholderInfo shareholderInfo)
        {
            Features = new List<Feature>();

            for (var i = 0; i < featureCount; i++)
                Features.Add(new Feature(RelevancyStatus.Relevant, FeatureStatus.NeedsExploration, true));

            this.Resource = resource;
            this.Shareholders = shareholderInfo;
            this.audience = audience;
            this.team = team;
        }

        public void UpgradeFeature(int featureID)
        {
            Features[featureID].Update();
            SpendResources(new TeamResource(50, 0, 0, 0, 0));
        }

        public void ExploreFeature(int featureID)
        {
            Features[featureID].Explore();
            SpendResources(new TeamResource(0, 0, 0, 10, 0));
        }

        public void SpendResources(TeamResource resource)
        {
            Resource.Spend(resource);
        }

        public void SellShareToNewInvestor(int share, int price, int buyerInvestorId)
        {
            Shareholders.AddShareholder(buyerInvestorId);
            Shareholders.EditShare(share, 0, buyerInvestorId, price);
            Resource.AddMoney(price);
        }

        public int GetProgrammingPointsProductionValue ()
        {
            return team.GetProgrammingPointsProduction();
        }
        public int GetManagerPointsProductionValue ()
        {
            return team.GetManagerPointsProduction();
        }
        public int GetSalesPointsProductionValue ()
        {
            return team.GetSalesPointsProduction();
        }

        public int GetIdeaPointsProductionValue ()
        {
            return team.GetIdeaPointsProduction() * audience.IdeaGainModifier();
        }

        internal void ProduceMonthlyResources()
        {
            TeamResource teamResource = team.ProduceMonthlyResources()
                .SetIdeaPoints(GetIdeaPointsProductionValue());

            Resource.AddTeamPoints(teamResource);
        }

        internal void RecomputeMoney()
        {
            long income = GetMonthlyIncome();
            long expenses = GetMonthlyExpense();

            long difference = income - expenses;

            Resource.AddMoney(difference);
        }

        long GetMonthlyExpense()
        {
            return 1000;
        }

        private long GetMonthlyIncome()
        {
            return audience.customers * 100;
        }

        internal uint ChurnClients()
        {
            return audience.RemoveChurnClients();
        }

        internal uint ConvertClientsToCustomers()
        {
            return audience.ConvertClientsToCustomers();
        }

        // Debugging
        public void PrintTechnologies()
        {
            for (var i = 0; i < Features.Count; i++)
                Debug.Log("----- TECH " + i + ": " + Features[i].GetLiteralFeatureStatus());
        }

        public void PrintShareholderInfo()
        {
            Shareholders.PrintAllShareholders();
        }

        public void PrintResources()
        {
            Resource.Print();
        }
    }
}

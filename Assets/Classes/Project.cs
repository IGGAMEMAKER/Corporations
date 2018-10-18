using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Project
    {
        List<Feature> Features;
        TeamResource Resource;

        Audience audience;
        Team team;

        public Project(int featureCount, Audience audience, Team team, TeamResource resource)
        {
            Features = new List<Feature>();

            for (var i = 0; i < featureCount; i++)
                Features.Add(new Feature(RelevancyStatus.Relevant, FeatureStatus.NeedsExploration, true));

            this.Resource = resource;
            this.audience = audience;
            this.team = team;
        }

        public void UpgradeFeature(int featureID)
        {
            Features[featureID].Update();
            Resource.Spend(new TeamResource(50, 0, 0, 0, 0));
        }

        public void ExploreFeature(int featureID)
        {
            Features[featureID].Explore();
            Resource.Spend(new TeamResource(0, 0, 0, 10, 0));
        }

        public int GetProgrammingPointsProductionValue()
        {
            return team.GetProgrammingPointsProduction();
        }

        public int GetManagerPointsProductionValue()
        {
            return team.GetManagerPointsProduction();
        }

        public int GetSalesPointsProductionValue()
        {
            return team.GetSalesPointsProduction();
        }

        public int GetIdeaPointsProductionValue()
        {
            return team.GetIdeaPointsProduction() * audience.IdeaGainModifier();
        }

        internal void UpdateMonthlyResources()
        {
            TeamResource teamResource = team.GetMonthlyResources()
                .SetIdeaPoints(GetIdeaPointsProductionValue());

            Resource.AddTeamPoints(teamResource);
        }

        internal void UpdateMonthlyMoney()
        {
            long income = GetMonthlyIncome();
            long expenses = GetMonthlyExpense();

            long difference = income - expenses;

            Resource.AddMoney(difference);
        }

        long GetMonthlyExpense()
        {
            return team.GetExpenses();
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

        internal void StartAdCampaign(uint clients)
        {
            audience.AddClients(clients);
        }

        // Debugging
        public void PrintTechnologies()
        {
            for (var i = 0; i < Features.Count; i++)
                Debug.Log("----- TECH " + i + ": " + Features[i].GetLiteralFeatureStatus());
        }

        public void PrintResources()
        {
            Resource.Print();
        }

        internal void PrintProjectInfo()
        {
            Debug.LogFormat("Project info: {0} customers, {1} clients", audience.customers, audience.clients);
        }
    }
}

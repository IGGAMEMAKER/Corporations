using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class Project
    {
        public int id { get; set; }
        public List<Feature> Features;
        TeamResource Resource;

        public List<Advert> Ads { get; set; }

        Audience audience;
        Team Team;

        public Project(int id, List<Feature> Features, Audience audience, Team Team, TeamResource resource, List<Advert> Ads)
        {
            this.Features = Features;
            this.Resource = resource;
            this.audience = audience;
            this.Team = Team;
            this.Ads = Ads;
            this.id = id;
        }

        internal List<Advert> GetAds()
        {
            return Ads;
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
            return Team.GetProgrammingPointsProduction();
        }

        public int GetManagerPointsProductionValue()
        {
            return Team.GetManagerPointsProduction();
        }

        public int GetSalesPointsProductionValue()
        {
            return Team.GetSalesPointsProduction();
        }

        public int GetIdeaPointsProductionValue()
        {
            return Team.GetIdeaPointsProduction() * audience.IdeaGainModifier();
        }

        internal void UpdateMonthlyResources()
        {
            TeamResource teamResource = Team.GetMonthlyResources()
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
            return Team.GetExpenses();
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

        void AddAdvertIfNotExist(int channelId)
        {
            if (Ads[channelId] == null)
                Ads[channelId] = new Advert(channelId, id, 0, 0);
        }

        internal Advert GetAdByChannelId(int channelId)
        {
            AddAdvertIfNotExist(channelId);

            return Ads[channelId];
        }

        internal void PrepareAd(int duration, int channelId)
        {
            if (Ads[channelId] == null)
                Ads[channelId] = new Advert(channelId, id, 0, duration);

            Ads[channelId].PrepareAd(duration);
        }
    }
}

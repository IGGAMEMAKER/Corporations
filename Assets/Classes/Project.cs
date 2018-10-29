using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public struct TeamMoraleData
    {
        public bool isMakingMoney;
        public bool isTopCompany;
        public bool isTeam;
        public bool isInnovative;

        public int teamSizePenalty;
        public int salaries;

        public int Morale
        {
            get
            {
                // set base value
                int value = Balance.MORALE_BONUS_BASE;

                if (isMakingMoney)
                    value += Balance.MORALE_BONUS_IS_PROFITABLE;

                if (isTopCompany)
                    value += Balance.MORALE_BONUS_IS_PRESTIGEOUS_COMPANY;

                if (isInnovative)
                    value += Balance.MORALE_BONUS_IS_INNOVATIVE;

                if (isTeam)
                    value += Balance.MORALE_BONUS_IS_TEAM;

                value += salaries;
                value -= teamSizePenalty;

                return value;
            }
        }
        
        public TeamMoraleData (bool isMakingMoney, bool isTopCompany, bool isTeam, bool isInnovative, int teamSizePenalty, int salaries)
        {
            this.isMakingMoney = isMakingMoney;
            this.isTopCompany = isTopCompany;
            this.isTeam = isTeam;
            this.isInnovative = isInnovative;

            this.teamSizePenalty = teamSizePenalty;
            this.salaries = salaries;
        }
    }
    public class Project
    {
        public int Id { get; set; }
        public List<Feature> Features;
        TeamResource Resource;

        public List<Advert> Ads { get; set; }

        public Audience audience;
        Team team;

        public Project(int id, List<Feature> Features, Audience audience, Team Team, TeamResource resource, List<Advert> Ads)
        {
            this.Features = Features;
            this.Resource = resource;
            this.audience = audience;
            this.team = Team;
            this.Ads = Ads;
            this.Id = id;
        }

        public TeamMoraleData moraleData {
            get
            {
                TeamMoraleData morale = new TeamMoraleData
                {
                    isMakingMoney = MoneyChange() > 0,
                    isTeam = team.Workers.Count > 1,
                    isTopCompany = false,
                    isInnovative = false,
                    salaries = 30,
                    teamSizePenalty = team.Workers.Count * 3
                };

                return morale;
            }
            internal set {}
        }

        public TeamResource resources
        {
            get { return Resource; }
            set {}
        }

        public uint Clients {
            get { return audience.clients; }
            internal set { }
        }

        public Team Team { get { return team; } internal set { } }

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

        long MoneyChange()
        {
            long income = GetMonthlyIncome();
            long expenses = GetMonthlyExpense();

            return income - expenses;
        }

        internal void UpdateMonthlyMoney()
        {
            long difference = MoneyChange();

            Resource.AddMoney(difference);
        }

        long GetMonthlyExpense()
        {
            return team.GetExpenses();
        }

        private long GetMonthlyIncome()
        {
            return audience.paidClients * 100;
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
            Debug.LogFormat("Project info: {0} customers, {1} clients", audience.paidClients, audience.clients);
        }

        Advert FindAdByChannelId(int channelId)
        {
            return Ads.Find(a => a.Channel == channelId);
        }

        void AddAdvertIfNotExist(int channelId)
        {
            Advert ad = FindAdByChannelId(channelId);

            if (ad == null)
                Ads.Add(new Advert(channelId, Id, 0, 0));
        }

        public Advert GetAdByChannelId(int channelId)
        {
            AddAdvertIfNotExist(channelId);

            return FindAdByChannelId(channelId);
        }

        internal void PrepareAd(int duration, int channelId)
        {
            GetAdByChannelId(channelId).PrepareAd(duration);
        }
    }
}

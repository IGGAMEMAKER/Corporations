using Assets.Utils;
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
                value += teamSizePenalty;

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
        public List<Feature> Features;
        TeamResource Resource;
        public Audience audience;
        Team team;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Advert> Ads { get; set; }
        public List<Human> Employees { get; set; }
        internal int stealingPotential;


        public TeamMoraleData moraleData
        {
            get
            {
                return new TeamMoraleData
                {
                    isMakingMoney = MoneyChange() > 0,
                    isTeam = team.Workers.Count > 1,
                    isTopCompany = false,
                    isInnovative = false,
                    salaries = 30,
                    teamSizePenalty = team.Workers.Count * Balance.MORALE_PENALTY_COST_PER_WORKER
                };
            }
            internal set { }
        }

        public TeamResource resourceMonthChanges
        {
            get
            {
                return MonthResourceChange().AddMoney(MoneyChange());
            }
            internal set { }
        }

        public TeamResource resources
        {
            get { return Resource; }
            set { }
        }

        public uint Clients
        {
            get { return audience.clients; }
            internal set { }
        }

        public Team Team { get { return team; } internal set { } }

        public Project(int id, string Name, List<Feature> Features, Audience audience, Team Team, TeamResource resource, List<Advert> Ads)
        {
            this.Features = Features;
            this.Resource = resource;
            this.audience = audience;
            this.team = Team;
            this.Ads = Ads;
            this.Id = id;
            this.Name = Name;

            UpdatdeEmployeeList();
        }

        public int ProgrammerAverageLevel()
        {
            return Team.GetProgrammerAverageLevel();
        }

        

        public Human CreateEmployee()
        {
            return EmployeeGenerator.Generate(Team);
        }

        public void UpdatdeEmployeeList()
        {
            int numberOfEmployees = 6;

            List<Human> candidates = new List<Human>();

            for (var i = 0; i < numberOfEmployees; i++)
                candidates.Add(CreateEmployee());

            Employees = candidates;
        }

        internal void FireWorker(int workerId)
        {
            Team.Fire(workerId);
        }

        internal void HireEmployee(int workerId)
        {
            Human employee = Employees[workerId];
            Team.Join(employee);

            Employees.RemoveAt(workerId);
        }

        internal List<Advert> GetAds()
        {
            return Ads;
        }

        public void UpgradeFeature(int featureID)
        {
            Features[featureID].Update();
            Resource.Spend(new TeamResource(50, 0, 0, 0, 0));

            UpgradeProgrammers(1);
        }

        public void ExploreFeature(int featureID)
        {
            Features[featureID].Explore();
            Resource.Spend(new TeamResource(0, 0, 0, 10, 0));

            UpgradeManagers(1);
            UpgradeProgrammers(1);
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

        TeamResource MonthResourceChange()
        {
            return team.GetMonthlyResources()
                .SetIdeaPoints(GetIdeaPointsProductionValue());
        }

        internal void UpdateMonthlyResources()
        {
            Resource.AddTeamPoints(MonthResourceChange());
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

        void UpgradeMarketers(float xpRatio = 1f)
        {
            Team.UpgradeMarketers(xpRatio);
        }

        void UpgradeProgrammers(float xpRatio = 1f)
        {
            Team.UpgradeProgrammers(xpRatio);
        }

        void UpgradeManagers(float xpRatio = 1f)
        {
            Team.UpgradeManagers(xpRatio);
        }

        long GetMonthlyExpense()
        {
            return team.GetExpenses();
        }

        private long GetMonthlyIncome()
        {
            return audience.clients * 100;
        }

        internal uint RemoveChurnClients()
        {
            return audience.RemoveChurnClients();
        }

        internal void StartAdCampaign(uint clients)
        {
            audience.AddClients(clients);

            UpgradeMarketers();
        }

        internal void PrepareAd(int duration, int channelId)
        {
            GetAdByChannelId(channelId).PrepareAd(duration);

            UpgradeMarketers(2);
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


        internal void ReceiveIdeas(int stealableIdeas)
        {
            Resource.AddIdeas(stealableIdeas);
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
    }
}

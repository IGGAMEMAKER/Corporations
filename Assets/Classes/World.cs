using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    public class World
    {
        public static int featureCount = 4;
        public int baseTechCost = 50;

        List<Market> Markets;
        List<Project> Projects;
        List<Channel> Channels;

        List<Investor> Investors;
        ScheduleManager schedule;

        public World()
        {
            int projectId = 0;
            int marketId = 0;

            InitializeShareholders();

            Projects = new List<Project>();
            FillProjects();

            InitializeMarkets(marketId);

            InitializeScheduleManager();
        }

        void InitializeShareholders()
        {
            Investors = GenerateInvestorPool();
            ShareInfo shareInfo = new ShareInfo(100, 0, 0);
            List<ShareInfo> shareholders = new List<ShareInfo> { shareInfo };
        }

        void FillProjects()
        {
            int[] mySkills = new int[] { 1, 7, 3 };
            int[] myTraits = new int[] { };
            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            List<Human> workers = new List<Human> { me };

            Audience audience = new Audience(0, 0, 1);

            Team team = new Team(workers);

            var Features = new List<Feature>();

            for (var i = 0; i < featureCount; i++)
                Features.Add(new Feature(RelevancyStatus.Relevant, FeatureStatus.NeedsExploration, true));

            List<Advert> Ads = new List<Advert>();

            // Add test ad
            Ads.Add(new Advert(0, 0, 0));

            Project p = new Project(Features, audience, team, teamResource, Ads);
            Projects.Add(p);
        }

        void InitializeMarkets(int marketId)
        {
            Markets = new List<Market>();
            Channels = new List<Channel>();

            MarketInfo marketInfo = new MarketInfo(baseTechCost, 1, 1, 50, featureCount);
            Markets.Add(new Market(marketInfo, new MarketSettings(10)));

            Channels.Add(new Channel(10, 10000, 10000, marketId));
        }

        void InitializeScheduleManager()
        {
            List<Task> tasks = new List<Task>
            {
                new Task(TaskType.ExploreFeature, 10, 10, new Dictionary<string, object>(), 1, 11)
            };
            schedule = new ScheduleManager(tasks, 0);
        }

        public Project GetProjectById(int projectId)
        {
            return Projects[projectId];
        }

        public Channel GetChannelById(int channelId)
        {
            return Channels[channelId];
        }

        public void PeriodTick(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                schedule.PeriodTick();

                if (schedule.IsPeriodEnd())
                    CalculatePeriodChanges();
            }
        }

        public string GetFormattedDate ()
        {
            return schedule.GetFormattedDate();
        }

        public void CalculatePeriodChanges ()
        {
            Debug.Log("Month tick!");

            for (var i = 0; i < Projects.Count; i++)
            {
                // recompute resources: money and team points
                UpdateResources(i);
                
                // recompute customers
                UpdateCustomers(i);
                
                // recompute clients: churn and ad campaigns
                UpdateClients(i);

                PrintProjectInfo(i);
            }
        }

        void UpdateResources (int projectId)
        {
            GetProjectById(projectId).UpdateMonthlyResources();
            GetProjectById(projectId).UpdateMonthlyMoney();

            PrintResources(projectId);
        }

        void UpdateCustomers (int projectId)
        {
            GetProjectById(projectId).ConvertClientsToCustomers();
        }

        void UpdateClients (int projectId)
        {
            uint churn = GetProjectById(projectId).ChurnClients();

            Debug.LogFormat("//TODO UpdateClients: return churn clients to Channels. {0}", churn);
        }

        List<Investor> GenerateInvestorPool()
        {
            List<Investor> list = new List<Investor>();
            int investorAmount = UnityEngine.Random.Range(3, 10);

            for (var i = 0; i < investorAmount; i++)
            {
                int rich = UnityEngine.Random.Range(1, 100);
                int money = rich * 100000;

                InvestorType investorType = rich < 60 ? InvestorType.Speculant : InvestorType.WantsDividends;

                list.Add(new Investor(money, investorType));
            }

            return list;
        }


        internal void StartAdCampaign(int projectId, int channelId)
        {
            Advert advert = GetProjectById(projectId).GetAdByChannelId(channelId);

            uint clients = GetChannelById(channelId).StartAdCampaign(projectId, advert);

            GetProjectById(projectId).StartAdCampaign(clients);
        }

        internal void PrepareAd(int projectId, int channelId, int duration)
        {
            GetProjectById(projectId).PrepareAd(duration, channelId);
        }

        public void UpgradeFeature(int projectId, int featureId)
        {
            GetProjectById(projectId).UpgradeFeature(featureId);
        }

        public void ExploreFeature(int projectId, int featureId)
        {
            GetProjectById(projectId).ExploreFeature(featureId);
        }

        // Debugging

        public void PrintTechnologies(int projectId)
        {
            GetProjectById(projectId).PrintTechnologies();
        }

        internal void PrintProjectInfo(int projectId)
        {
            GetProjectById(projectId).PrintProjectInfo();
        }

        public void PrintResources(int projectId)
        {
            GetProjectById(projectId).PrintResources();
        }
    }
}

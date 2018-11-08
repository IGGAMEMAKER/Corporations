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

        internal List<Project> projects {
            get { return Projects; }
            set {}
        }

        public World()
        {
            int marketId = 0;

            InitializeShareholders();

            Projects = new List<Project>();
            FillProjects();

            InitializeMarkets(marketId);

            InitializeScheduleManager();
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

        void InitializeShareholders()
        {
            Investors = GenerateInvestorPool();
            ShareInfo shareInfo = new ShareInfo(100, 0, 0);
            List<ShareInfo> shareholders = new List<ShareInfo> { shareInfo };
        }

        void FillProjects()
        {
            Skillset mySkills = new Skillset()
                .SetManagementLevel(3)
                .SetMarketingLevel(1)
                .SetProgrammingLevel(7);

            Skillset skillset = new Skillset()
                .SetManagementLevel(2)
                .SetMarketingLevel(7)
                .SetProgrammingLevel(0);

            int[] myTraits = new int[] { };
            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, WorkerSpecialisation.Programmer, 500);
            Human someoneElse = new Human("John", "Stones", skillset, myTraits, WorkerSpecialisation.Marketer, 1500);
            Human someoneElse1 = new Human("John", "Stones", skillset, myTraits, WorkerSpecialisation.Marketer, 1500);
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            List<Human> workers = new List<Human> { me };

            Audience audience = new Audience(0, 1);

            Team team = new Team(workers);

            var Features = new List<Feature>();

            for (var i = 0; i < featureCount; i++)
                Features.Add(new Feature(String.Format("Feature {0}", i), RelevancyStatus.Relevant, FeatureStatus.NeedsExploration, true));

            List<Advert> Ads = new List<Advert>();

            Project p = new Project(0, "Startup Manager", Features, audience, team, teamResource, Ads);
            Project p1 = new Project(1, "Online Tournaments", Features, audience, team, teamResource, Ads);
            Project p2 = new Project(2, "Super Football", Features, audience, team, teamResource, Ads);
            Project p3 = new Project(3, "Medieval RPG", Features, audience, team, teamResource, Ads);

            Projects.Add(p);
            Projects.Add(p1);
            Projects.Add(p2);
            Projects.Add(p3);

            // Add test ads
            p.PrepareAd(0, 0);
            p.PrepareAd(0, 1);
        }

        void InitializeMarkets(int marketId)
        {
            Markets = new List<Market>();
            Channels = new List<Channel>();

            MarketInfo marketInfo = new MarketInfo(baseTechCost, 1, 1, 50, featureCount);
            Markets.Add(new Market(marketInfo, new MarketSettings(10)));

            Channels.Add(new Channel(10, 10000, 10000, marketId));
            Channels.Add(new Channel(10, 100000, 100000, marketId));
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

        public bool PeriodTick(int count = 1)
        {
            bool redraw = false;

            for (var i = 0; i < count; i++)
            {
                schedule.PeriodTick();

                if (schedule.IsPeriodEnd())
                {
                    CalculatePeriodChanges();
                    redraw = true;
                }
            }

            return redraw;
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
                
                // recompute clients: churn and ad campaigns
                UpdateClients(i);
            }
        }


        void UpdateResources(int projectId)
        {
            GetProjectById(projectId).UpdateMonthlyResources();
            GetProjectById(projectId).UpdateMonthlyMoney();
        }

        void UpdateClients(int projectId)
        {
            GetProjectById(projectId).RemoveChurnClients();
        }

        public void StealIdeas(int projectId, int targetProjectId)
        {
            Project reciever = GetProjectById(projectId);
            Project target = GetProjectById(targetProjectId);

            int stealableIdeas = target.stealingPotential;

            reciever.ReceiveIdeas(stealableIdeas);
        }

        public void StartAdCampaign(int projectId, int channelId)
        {
            Advert advert = GetProjectById(projectId).GetAdByChannelId(channelId);

            uint clients = GetChannelById(channelId).StartAdCampaign(projectId, advert);

            GetProjectById(projectId).StartAdCampaign(clients);
        }

        public void PrepareAd(int projectId, int channelId, int duration)
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

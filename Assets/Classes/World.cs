using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class World
    {
        public static int featureCount = 4;
        public int baseTechCost = 50;

        List<Project> Projects;

        List<Market> Markets;
        List<Channel> Channels;

        List<Investor> Investors;
        ScheduleManager schedule;

        public World()
        {
            int projectId = 0;
            int[] mySkills = new int[] { 1, 7, 3 };
            int[] myTraits = new int[] { };
            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);
            ShareInfo shareInfo = new ShareInfo(100, 0, 0);
            MarketInfo marketInfo = new MarketInfo(baseTechCost, 1, 1, 50, featureCount);
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            Investors = GenerateInvestorPool();

            List<Human> workers = new List<Human> { me };

            List<ShareInfo> shareholders = new List<ShareInfo> { shareInfo };

            Project p = new Project(featureCount, 0, workers, teamResource, new ShareholderInfo(shareholders));
            Projects = new List<Project> { p };

            Dictionary<int, Advert> adverts = new Dictionary<int, Advert>
            {
                { projectId, new Advert() }
            };

            Markets = new List<Market>
            {
                new Market(marketInfo, new MarketSettings(10))
            };

            int marketId = 0;
            Channels = new List<Channel>
            {
                new Channel(10, 10000, 10000, adverts, marketId)
            };


            List<Task> tasks = new List<Task>
            {
                new Task(Task.TaskType.ExploreFeature, 10, 10, new Dictionary<string, object>(), 1, 11)
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
            for (var i = 0; i < Projects.Count; i++)
            {
                // recompute resources: money and team points
                UpdateResources(i);
                // recompute customers
                UpdateCustomers(i);
                // recompute clients: churn and ad campaigns
                UpdateClients(i);
            }
        }

        void UpdateResources (int projectId)
        {
            GetProjectById(projectId).ProduceMonthlyResources();
            GetProjectById(projectId).RecomputeMoney();
        }

        void UpdateCustomers (int projectId)
        {
            GetProjectById(projectId).ConvertClientsToCustomers();
        }

        void UpdateClients (int projectId)
        {
            GetProjectById(projectId).ChurnClients();
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


        internal void RaiseInvestments(int projectId, int share, int price)
        {
            GetProjectById(projectId).SellShareToNewInvestor(share, price, 1);
        }

        internal void StartAdCampaign(int projectId, int channelId)
        {
            GetChannelById(channelId).StartAdCampaign(projectId);
        }

        internal void PrepareAd(int projectId, int channelId, int duration)
        {
            GetChannelById(channelId).PrepareAd(projectId, duration);
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

        internal void PrintShareholders(int projectId)
        {
            GetProjectById(projectId).PrintShareholderInfo();
        }
        public void PrintTechnologies(int projectId)
        {
            GetProjectById(projectId).PrintTechnologies();
        }

        internal void PrintProjectInfo(int projectId, int channelId)
        {
            GetChannelById(channelId).PrintProjectInfo(projectId);
        }

        public void PrintResources(int projectId)
        {
            GetProjectById(projectId).PrintResources();
        }
    }
}

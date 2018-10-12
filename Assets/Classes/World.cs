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
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            Investors = GenerateInvestorPool();

            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);
            List<Human> workers = new List<Human> { me };

            List<ShareInfo> shareholders = new List<ShareInfo>
            {
                new ShareInfo(100, 0, 0)
            };

            Project p = new Project(featureCount, workers, teamResource, new ShareholderInfo(shareholders));
            Projects = new List<Project> { p };

            Dictionary<int, ProjectRecord> projectRecords = new Dictionary<int, ProjectRecord> ();
            projectRecords.Add(projectId, new ProjectRecord());

            Channels = new List<Channel> { new Channel(10, 10000, 10000, projectRecords) };

            schedule = new ScheduleManager(new List<Task> {
                new Task(Task.TaskType.ExploreFeature, 10, 10, new Dictionary<string, object>(), 1, 11)
            }, 0);
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
            }
        }

        internal void PrintShareholders(int projectId)
        {
            GetProjectById(projectId).PrintShareholderInfo();
        }

        List<Investor> GenerateInvestorPool ()
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

        public void PrintTechnologies(int projectId)
        {
            GetProjectById(projectId).PrintTechnologies();
        }

        internal void PrintProjectInfo(int projectId, int channelId)
        {
            GetChannelById(channelId).PrintProjectInfo(projectId);
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

        public void PrintResources(int projectId)
        {
            GetProjectById(projectId).PrintResources();
        }
    }
}

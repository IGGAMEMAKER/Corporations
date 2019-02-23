using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public class World
    {
        public static int featureCount = 4;
        public int baseTechCost = 50;

        List<Market> Markets;
        List<ProductComponent> Projects;
        List<Channel> Channels;

        List<Investor> Investors;
        ScheduleManager schedule;

        internal List<ProductComponent> projects {
            get { return Projects; }
            set {}
        }

        public World()
        {
            int marketId = 0;

            InitializeShareholders();

            Projects = new List<ProductComponent>();
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

            int[] myTraits = new int[] { };
            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, WorkerSpecialisation.Programmer, 500);

            List<Human> workers = new List<Human> { me };

            Audience audience = new Audience(0, 1);

            Team team = new Team(workers);

            List<Advert> Ads = new List<Advert>();

            ProductComponent p = ProductGenerator("Startup Manager");
            //Project p1 = new Project(1, "Online Tournaments", Features, audience, team, teamResource, Ads);
            //Project p2 = new Project(2, "Super Football", Features, audience, team, teamResource, Ads);
            //Project p3 = new Project(3, "Medieval RPG", Features, audience, team, teamResource, Ads);

            //Projects.Add(p);
        }

        ProductComponent ProductGenerator (string name)
        {
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            return new ProductComponent {
                Id = 0, Name = name, Ads = new List<Advert>(),
                Analytics = 0,
                BrandPower = 100,
                Clients = 100,
                ExperimentCount = 1, ExplorationLevel = 0, Level = 1,
                Managers = 1, Marketers = 1, Programmers = 1,
                Niche = Niche.Messenger,
                Resources = teamResource
            };
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

        internal void ExchangeShare(int sellerId, int buyerId, int share)
        {
            Debug.LogFormat("ExchangeShare: (sellerId: {0}, buyerId: {1}, share: {2}%)", sellerId, buyerId, share);
            //throw new NotImplementedException();
        }

        public ProductComponent GetProjectById(int projectId)
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
                    //CalculatePeriodChanges();
                    redraw = true;
                }
            }

            return redraw;
        }

        public string GetFormattedDate ()
        {
            return schedule.GetFormattedDate();
        }
    }
}

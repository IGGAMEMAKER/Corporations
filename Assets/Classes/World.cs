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
        public int maxAmountOfTraits = 3;

        List<Project> Projects;
        List<Market> Markets;
        List<Channel> Channels;


        public World()
        {
            int projectId = 0;
            int[] mySkills = new int[] { 1, 7, 3 };
            int[] myTraits = new int[] { };
            TeamResource teamResource = new TeamResource(100, 100, 100, 10, 5000);

            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);
            List<Human> workers = new List<Human> { me };

            Project p = new Project(featureCount, workers, teamResource);
            Projects = new List<Project> { p };

            Dictionary<int, ProjectRecord> projectRecords = new Dictionary<int, ProjectRecord> ();
            projectRecords.Add(projectId, new ProjectRecord());

            Channels = new List<Channel> { new Channel(10, 10000, 10000, projectRecords) };
        }

        public void PrintTechnologies(int projectId)
        {
            GetProjectById(projectId).PrintTechnologies();
        }

        internal void PrintProjectInfo(int projectId, int channelId)
        {
            Channels[channelId].PrintProjectInfo(projectId);
        }

        internal void PeriodTick(int projectId)
        {
            throw new NotImplementedException();
        }

        internal void StartAd(int projectId, int channelId)
        {
            Channels[channelId].InvokeAdCampaign(projectId);
        }

        internal void PrepareAd(int projectId, int channelId, int duration)
        {
            Channels[channelId].PrepareAd(projectId, duration);
        }

        public Project GetProjectById (int projectId)
        {
            return Projects[projectId];
        }

        public void UpgradeFeature (int projectId, int featureId)
        {
            GetProjectById(projectId).UpgradeFeature(featureId);
        }

        public void ExploreFeature(int projectId, int featureId)
        {
            GetProjectById(projectId).ExploreFeature(featureId);
        }

        public void PrintResources (int projectId)
        {
            GetProjectById(projectId).PrintResources();
        }
    }
}

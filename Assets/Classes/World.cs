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


        public World ()
        {
            int[] mySkills = new int[] { 1, 7, 3 };
            int[] myTraits = new int[] { };
            Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);

            List<Human> workers = new List<Human> { me };
            Project p = new Project(featureCount, workers, new TeamResource(100, 100, 100, 10, 5000));

            Projects = new List<Project> { p };
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

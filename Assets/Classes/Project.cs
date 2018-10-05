using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class Project
    {
        List<int> Features;
        List<Human> Workers;
        TeamResource teamResource;

        public Project(int featureCount, List<Human> workers, TeamResource resource)
        {
            Features = Enumerable.Repeat(0, featureCount).ToList();
            Workers = workers;
            teamResource = resource;
        }

        void UpgradeFeature(int featureID)
        {

            Features[featureID]++;
        }

        void SpendResources(TeamResource resource)
        {
            teamResource.Spend(resource);
        }

        void PrintFeatures()
        {
            Debug.Log("Printing elements!");
            for (int i = 0; i < Features.Count; i++)
            {
                Debug.Log("Printing element[" + i + "]: " + Features[i]);
            }
        }
    }
}

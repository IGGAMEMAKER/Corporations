using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    class Project
    {
        List<Feature> Features;
        List<Human> Workers;
        TeamResource Resource;

        public Project (int featureCount, List<Human> workers, TeamResource resource)
        {
            Features = Enumerable
                .Repeat(new Feature (RelevancyStatus.Relevant, FeatureStatus.NeedsExploration), featureCount)
                .ToList();
            Workers = workers;
            Resource = resource;
        }

        public void UpgradeFeature (int featureID)
        {
            Features[featureID].Update();
            SpendResources(new TeamResource(50, 0, 0, 10, 0));
        }

        public void ExploreFeature (int featureID)
        {
            Features[featureID].Explore();
            SpendResources(new TeamResource(0, 0, 0, 10, 0));
        }

        public void SpendResources (TeamResource resource)
        {
            Resource.Spend(resource);
        }

        public void PrintFeatures()
        {
            Debug.Log("Printing features!");

            for (int i = 0; i < Features.Count; i++)
            {
                Debug.Log("Printing feature[" + i + "]: ");
                Debug.Log(Features[i]);
                Debug.Log("---------------------");
            }
        }
    }
}

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
        ShareholderInfo Shareholders;
        TeamResource Resource;

        public Project(int featureCount, List<Human> workers, TeamResource resource, ShareholderInfo shareholderInfo)
        {
            Features = new List<Feature>();
            for (var i = 0; i < featureCount; i++)
            {
                Features.Add(new Feature(RelevancyStatus.Relevant, FeatureStatus.NeedsExploration));
            }
            Workers = workers;
            Resource = resource;
            Shareholders = shareholderInfo;
        }

        public void UpgradeFeature(int featureID)
        {
            Features[featureID].Update();
            SpendResources(new TeamResource(50, 0, 0, 0, 0));
        }

        public void ExploreFeature(int featureID)
        {
            Features[featureID].Explore();
            SpendResources(new TeamResource(0, 0, 0, 10, 0));
        }

        public void SpendResources(TeamResource resource)
        {
            Resource.Spend(resource);
        }

        public void SellShareToNewInvestor(int share, int price, int buyerInvestorId)
        {
            Shareholders.AddShareholder(buyerInvestorId);
            Shareholders.EditShare(share, 0, buyerInvestorId, price);
            Resource.AddMoney(price);
        }

        public void PrintTechnologies ()
        {            
            for (var i = 0; i < Features.Count; i++)
            {
                Debug.Log("----- TECH " + i + ": " + Features[i].GetLiteralFeatureStatus());
            }
        }

        public void PrintShareholderInfo ()
        {
            Shareholders.PrintAllShareholders();
        }

        public void PrintResources ()
        {
            Resource.Print();
        }
    }
}

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

        uint clients;
        uint customers;

        public Project(int featureCount, uint clients, uint customers, List<Human> workers, TeamResource resource, ShareholderInfo shareholderInfo)
        {
            Features = new List<Feature>();

            for (var i = 0; i < featureCount; i++)
                Features.Add(new Feature(RelevancyStatus.Relevant, FeatureStatus.NeedsExploration));

            Workers = workers;
            Resource = resource;
            Shareholders = shareholderInfo;
            this.clients = clients;
            this.customers = customers;
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

        public int GetProgrammingPointsProductionValue ()
        {
            return 100;
        }
        public int GetManagerPointsProductionValue ()
        {
            return 100;
        }
        public int GetSalesPointsProductionValue ()
        {
            return 100;
        }
        public int GetIdeaPointsProductionValue ()
        {
            return 100;
        }


        public float GetConversionRate ()
        {
            return 0.05f;
        }

        public float GetChurnRate()
        {
            return 0.05f;
        }

        public uint NewCustomersAmount ()
        {
            return (uint) Math.Floor(clients * GetConversionRate());
        }

        public int CustomerAnalyticsModifier ()
        {
            if (customers > 100000)
                return 6;
            else if (customers > 10000)
                return 5;
            else if (customers > 1000)
                return 4;
            else if (customers > 100)
                return 3;
            else
                return 2;
        }

        public int ClientAnalyticsModifier ()
        {
            if (clients > 1000000)
                return 6;
            else if (clients > 100000)
                return 5;
            else if (clients > 10000)
                return 4;
            else if (clients > 1000)
                return 3;
            else
                return 2;
        }

        public int IdeaGainModifier()
        {
            int modifier = 0;

            int amountOfTests = 1; // it depends on client amount. 100, 1000, 100000
            int analyticsQuality = 2;
            int headBonus = 2;

            modifier = amountOfTests + analyticsQuality + headBonus;

            return modifier;
        }

        internal void ProduceMonthlyResources()
        {
            TeamResource teamResource = new TeamResource()
                .SetProgrammingPoints(GetProgrammingPointsProductionValue())
                .SetManagerPoints(GetManagerPointsProductionValue())
                .SetSalesPoints(GetSalesPointsProductionValue())
                .SetIdeaPoints(GetIdeaPointsProductionValue());

            Resource.AddTeamPoints(teamResource);
        }

        internal void RecomputeMoney()
        {
            long income = GetMonthlyIncome();
        }

        private long GetMonthlyIncome()
        {
            return customers * 100;
        }

        internal uint ChurnClients()
        {
            var churnRate = GetChurnRate();
            uint churnClients = (uint)(churnRate * clients);

            clients -= churnClients;

            return churnClients;
        }

        internal void ConvertClientsToCustomers()
        {
            var conversionRate = GetConversionRate();
            uint newCustomers = (uint) (conversionRate * clients);

            customers += newCustomers;
            clients -= newCustomers;
        }

        // Debugging
        public void PrintTechnologies()
        {
            for (var i = 0; i < Features.Count; i++)
                Debug.Log("----- TECH " + i + ": " + Features[i].GetLiteralFeatureStatus());
        }

        public void PrintShareholderInfo()
        {
            Shareholders.PrintAllShareholders();
        }

        public void PrintResources()
        {
            Resource.Print();
        }
    }
}

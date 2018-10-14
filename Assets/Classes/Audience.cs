using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Audience
    {
        public uint clients;
        public uint customers;

        int AmountOfTests = 1;

        public Audience(uint clients, uint customers, int AmountOfTests = 1) {
            this.clients = clients;
            this.customers = customers;
            this.AmountOfTests = AmountOfTests;
        }

        internal uint ConvertClientsToCustomers()
        {
            var conversionRate = GetConversionRate();
            uint newCustomers = (uint)(conversionRate * clients);

            customers += newCustomers;
            clients -= newCustomers;

            return newCustomers;
        }

        internal uint RemoveChurnClients()
        {
            var churnRate = GetChurnRate();
            uint churnClients = (uint)(churnRate * clients);

            clients -= churnClients;

            return churnClients;
        }

        public float GetConversionRate()
        {
            return 0.05f;
        }

        public float GetChurnRate()
        {
            return 0.15f;
        }

        public uint NewCustomersAmount()
        {
            return (uint) Math.Floor(clients * GetConversionRate());
        }

        public int CustomerAnalyticsCap()
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

        public int ClientAnalyticsCap()
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

        public void IncreaseAmountOfTests()
        {
            int max = ClientAnalyticsCap() + CustomerAnalyticsCap();

            if (AmountOfTests < max)
                AmountOfTests++;
        }

        public void DecreaseAmountOfTests()
        {
            if (AmountOfTests > 1)
                AmountOfTests--;
        }

        public void CheckAmountOfTests()
        {
            int max = ClientAnalyticsCap() + CustomerAnalyticsCap();

            if (max < AmountOfTests)
                AmountOfTests = max;
        }

        public int IdeaGainModifier()
        {
            int modifier = 0;

            int analyticsQuality = 2;

            modifier = AmountOfTests + analyticsQuality;

            return modifier;
        }

        internal void AddClients(uint clients)
        {
            this.clients += clients;
        }
    }
}

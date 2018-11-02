using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public struct ClientChangeInfo
    {
        public uint churnClients;
        public uint convertedClients;
        public float churnRate;
        public float conversionRate;

        public ClientChangeInfo(uint churnClients, uint convertedClients, float churnRate, float conversionRate)
        {
            this.churnClients = churnClients;
            this.convertedClients = convertedClients;
            this.churnRate = churnRate;
            this.conversionRate = conversionRate;
        }
    }
    public class Audience
    {
        public uint clients;
        public uint paidClients;

        int AmountOfTests = 1;

        public ClientChangeInfo GetMonthChange()
        {
            return new ClientChangeInfo(GetChurnClients(), GetNewPaidClients(), GetChurnRate(), GetConversionRate());
        }

        public Audience(uint clients, uint customers, int AmountOfTests = 1) {
            this.clients = clients;
            this.paidClients = customers;
            this.AmountOfTests = AmountOfTests;
        }

        uint GetNewPaidClients ()
        {
            var conversionRate = GetConversionRate();

            return (uint)(conversionRate * clients);
        }

        uint GetChurnClients ()
        {
            var churnRate = GetChurnRate();

            return (uint)(churnRate * clients);
        }

        internal uint ConvertClientsToCustomers()
        {
            uint newPaidClients = GetNewPaidClients();

            paidClients += newPaidClients;
            clients -= newPaidClients;

            return newPaidClients;
        }

        internal uint RemoveChurnClients()
        {
            uint churnClients = GetChurnClients();

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
            if (paidClients > 100000)
                return 6;
            else if (paidClients > 10000)
                return 5;
            else if (paidClients > 1000)
                return 4;
            else if (paidClients > 100)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    class Channel
    {
        int MaxClients;
        int Clients;
        int Engagement; // 1 ... 5
        int marketId;

        Dictionary<int, Advert> Adverts; // int - projectID

        public Channel(int engagement, int maxClients, int clients, Dictionary<int, Advert> adverts, int marketId)
        {
            MaxClients = maxClients;
            Clients = clients;
            Engagement = engagement;
            Adverts = adverts;
            this.marketId = marketId;
        }

        void CreateAdIfNotExist (int projectId)
        {
            if (Adverts[projectId] == null)
                Adverts[projectId] = new Advert(0, 0);
        }

        public int StartAdCampaign(int projectId)
        {
            CreateAdIfNotExist(projectId);

            float adEffeciency = Adverts[projectId].AdEffeciency / 100f;
            float dice = UnityEngine.Random.Range(Balance.advertClientsRangeMin, Balance.advertClientsRangeMax) / 100f;
            int clients = (int) (Engagement * adEffeciency * Clients * dice / 100);

            string info = String.Format("added {0} clients (possible: {1} . {2} dice {3} effeciency {4}) to {5} via last campaign",
                clients, Clients, Engagement, dice, adEffeciency, projectId);
            Debug.Log(info);

            Clients -= clients;

            return clients;
        }

        internal void PrepareAd(int projectId, int duration)
        {
            CreateAdIfNotExist(projectId);

            Adverts[projectId].PrepareAd(duration);
        }

        public void PrintProjectInfo(int projectId)
        {
            Debug.Log("Clients: PrintProjectInfo Useless");
        }
    }
}
//using System;
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
        int Engagement; // 0.01 ... 0.05
        Dictionary<int, ChannelInfo> ChannelInfos; // string - projectID

        public Channel(int engagement, int maxClients, int clients, Dictionary<int, ChannelInfo> channelInfos)
        {
            MaxClients = maxClients;
            Clients = clients;
            Engagement = engagement;
            ChannelInfos = channelInfos;
        }

        public int InvokeAdCampaign(int projectID, float effeciency)
        {
            int clients = (int)Mathf.Floor(Engagement * effeciency * Clients * Random.Range(1, 2));
            Debug.Log("added " + clients.ToString() + " clients to " + projectID + " via last campaign");

            if (ChannelInfos[projectID] == null)
                ChannelInfos[projectID] = new ChannelInfo(clients);
            else
                ChannelInfos[projectID].AddClients(clients);

            return clients;
        }
    }

    class ChannelInfo
    {
        int Clients;

        public ChannelInfo(int clients)
        {
            Clients = clients;
        }

        public void AddClients(int clients)
        {
            Clients += clients;
        }
    }
}

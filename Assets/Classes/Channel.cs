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

        Dictionary<int, ProjectRecord> ProjectRecords; // int - projectID

        public Channel(int engagement, int maxClients, int clients, Dictionary<int, ProjectRecord> records)
        {
            MaxClients = maxClients;
            Clients = clients;
            Engagement = engagement;
            ProjectRecords = records;
        }

        void CreateProjectRecord (int projectId)
        {
            if (ProjectRecords[projectId] == null)
                ProjectRecords[projectId] = new ProjectRecord(0, 0, 0);
        }

        public int StartAdCampaign(int projectId)
        {
            CreateProjectRecord(projectId);

            float adEffeciency = ProjectRecords[projectId].AdEffeciency / 100f;
            float dice = UnityEngine.Random.Range(Balance.advertClientsRangeMin, Balance.advertClientsRangeMax) / 100f;
            int clients = (int) (Engagement * adEffeciency * Clients * dice / 100);

            string info = String.Format("added {0} clients (possible: {1} . {2} dice {3} effeciency {4}) to {5} via last campaign",
                clients, Clients, Engagement, dice, adEffeciency, projectId);
            Debug.Log(info);

            ProjectRecords[projectId].AddClients(clients);

            return clients;
        }

        internal void PrepareAd(int projectId, int duration)
        {
            CreateProjectRecord(projectId);

            ProjectRecords[projectId].PrepareAd(duration);
        }

        public void PrintProjectInfo(int projectId)
        {
            Debug.Log("Clients: " + ProjectRecords[projectId].Clients);
        }
    }

    class ProjectRecord
    {
        public int Clients { get; set; }
        public int AdEffeciency { get; set; } 
        public int AdDuraion { get; set; }
        public bool IsRunningCampaign { get; set; }

        public ProjectRecord(int clients = 0, int adEffeciency = 0, int adDuration = 0)
        {
            Clients = clients;
            AdEffeciency = adEffeciency;
            AdDuraion = adDuration;
        }

        public void AddClients(int clients)
        {
            Clients += clients;
        }

        public void PrepareAd(int duration)
        {
            AdEffeciency = UnityEngine.Random.Range(Balance.advertEffeciencyRangeMin, Balance.advertEffeciencyRangeMax);
            AdDuraion = duration;
            IsRunningCampaign = false;
        }
    }

    class ChannelSettings
    {
        //public ChannelSettings ()
    }
}

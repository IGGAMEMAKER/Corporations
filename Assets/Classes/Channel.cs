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
        int Engagement; // 0.01 ... 0.05

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

        public int InvokeAdCampaign(int projectId)
        {
            float effeciency = ProjectRecords[projectId].AdEffeciency / 100f;
            float dice = UnityEngine.Random.Range(100, 175) / 100f;
            int clients = (int) (Engagement * effeciency * Clients * dice);

            string info = String.Format("added {0} clients (possible: {1} . {2} dice {3} effeciency {4}) to {5} via last campaign",
                clients, Clients, Engagement, dice, effeciency, projectId);
            Debug.Log(info);

            CreateProjectRecord(projectId);
            ProjectRecords[projectId].AddClients(clients);

            return clients;
        }        

        internal void PrepareAd(int projectId, int duration)
        {
            CreateProjectRecord(projectId);
            ProjectRecords[projectId].UpdateAd(duration);
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

        public void UpdateAd(int duration)
        {
            AdEffeciency = UnityEngine.Random.Range(10, 100);
            AdDuraion = duration;
            IsRunningCampaign = false;
        }
    }

    class ChannelSettings
    {
        //public ChannelSettings ()
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {
    public static int featureCount = 4;
    public int baseTechCost = 50;
    public int maxAmountOfTraits = 3;

    List<Project> Projects;

    // Use this for initialization
    void Start () {
        int[] mySkills = new int[] { 1, 7, 3 };
        int[] myTraits = new int[] { };
        Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);

        List<Human> workers = new List<Human>
        {
            me
        };
        Project p = new Project(featureCount, workers);

        Projects = new List<Project>{
            p
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    class Human
    {
        string Name;
        string Surname;
        int[] Skills;
        int[] Character;
        int Specialisation;
        int Salary;
        int Loyalty;

        public Human (string name, string surname, int[] skills, int[] character, int specialisation, int salary)
        {
            Name = name;
            Surname = surname;
            Skills = skills;
            Character = character;
            Specialisation = specialisation;
            Salary = salary;

            Loyalty = 100;
        }

        void UpgradeSkill (int skill)
        {
            Skills[skill]++;
        }

        void UpgradeBestSkill ()
        {
            UpgradeSkill(Specialisation);
        }
    }

    public class TeamResource
    {
        int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints;

        public TeamResource (int programmingPoints, int managerPoints, int salesPoints, int ideaPoints)
        {
            ProgrammingPoints = programmingPoints;
            ManagerPoints = managerPoints;
            SalesPoints = salesPoints;
            IdeaPoints = ideaPoints;
        }

        static bool IsEnoughResources (TeamResource Need)
        {
            Debug.LogError("IsEnoughResources not implemented");
            return false;
        }
    }

    class Project
    {
        List<int> Features;
        List<Human> Workers;
        TeamResource resource;

        public Project (int featureCount, List<Human> workers)
        {
            Features = Enumerable.Repeat(0, featureCount).ToList();
            Workers = workers;
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

    class Channel
    {
        int MaxClients;
        int Clients;
        int Engagement; // 0.01 ... 0.05
        Dictionary<string, ChannelInfo> ChannelInfos; // string - projectID

        public Channel (int engagement, int maxClients, int clients, Dictionary<string, ChannelInfo> channelInfos)
        {
            MaxClients = maxClients;
            Clients = clients;
            Engagement = engagement;
            ChannelInfos = channelInfos;
        }

        public int InvokeAdCampaign (string projectID, float effeciency)
        {
            int clients = (int) Mathf.Floor(Engagement * effeciency * Clients * Random.Range(1, 2));
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

        public ChannelInfo (int clients)
        {
            Clients = clients;
        }

        public void AddClients (int clients)
        {
            Clients += clients;
        }
    }
}

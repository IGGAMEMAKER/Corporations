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
        Project p = new Project(featureCount, workers, new TeamResource(100, 100, 100, 10, 5000));

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
        int ProgrammingPoints, ManagerPoints, SalesPoints, IdeaPoints, Money;

        public TeamResource (int programmingPoints, int managerPoints, int salesPoints, int ideaPoints, int money)
        {
            ProgrammingPoints = programmingPoints;
            ManagerPoints = managerPoints;
            SalesPoints = salesPoints;
            IdeaPoints = ideaPoints;
            Money = money;
        }

        static bool IsEnoughResources (TeamResource Need)
        {
            Debug.LogError("IsEnoughResources not implemented");

            return false;
        }
    }

    const int RELEVANCY_RELEVANT = 0;
    const int RELEVANCY_SLIGHTLY_OUTDATED = -1;
    const int RELEVANCY_VASTLY_OUTDATED = -2;

    enum FeatureStatus
    {
        NeedsExploration,
        Explored
    }

    class Feature
    {
        int Relevancy;
        FeatureStatus Status;

        public Feature (int relevancy, FeatureStatus status)
        {
            Status = status;
            Relevancy = relevancy;
        }

        public void Explore ()
        {
            Status = FeatureStatus.Explored;
        }

        public void Update ()
        {
            Status = FeatureStatus.NeedsExploration;
            Relevancy = RELEVANCY_RELEVANT;
        }

        public void Outdate ()
        {
            if (Relevancy == RELEVANCY_RELEVANT)
                Relevancy = RELEVANCY_SLIGHTLY_OUTDATED;
            else
                Relevancy = RELEVANCY_VASTLY_OUTDATED;

            Status = FeatureStatus.NeedsExploration;
        }
    }

    class Project
    {
        List<int> Features;
        List<Human> Workers;
        TeamResource teamResource;

        public Project (int featureCount, List<Human> workers, TeamResource resource)
        {
            Features = Enumerable.Repeat(0, featureCount).ToList();
            Workers = workers;
            teamResource = resource;
        }

        void UpgradeFeature (int featureID)
        {

            Features[featureID]++;
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

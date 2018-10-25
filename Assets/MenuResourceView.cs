using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuResourceView : MonoBehaviour {
    bool loaded = false;
    GameObject MoneyResourceView;
    GameObject ProgrammingPointsResourceView;
    GameObject SalesPointsResourceView;
    GameObject ManagerPointsResourceView;
    GameObject IdeaPointsResourceView;
    GameObject ClientResourceView;
    GameObject CustomerResourceView;
    GameObject ScheduleResourceView;

    // Use this for initialization
    void Start () {
    }

    void LoadViews()
    {
        MoneyResourceView = gameObject.transform.GetChild(0).gameObject;
        ProgrammingPointsResourceView = gameObject.transform.GetChild(1).gameObject;
        SalesPointsResourceView = gameObject.transform.GetChild(2).gameObject;
        ManagerPointsResourceView = gameObject.transform.GetChild(3).gameObject;
        IdeaPointsResourceView = gameObject.transform.GetChild(4).gameObject;
        CustomerResourceView = gameObject.transform.GetChild(5).gameObject;
        ClientResourceView = gameObject.transform.GetChild(6).gameObject;
        ScheduleResourceView = gameObject.transform.GetChild(7).gameObject;
        loaded = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RedrawResources(TeamResource teamResource, Audience audience, string currentDate)
    {
        if (!loaded)
            LoadViews();

        ResourceView moneyView = MoneyResourceView.GetComponent<ResourceView>();
        moneyView.UpdateResourceValue(teamResource.money);

        ResourceView ppView = ProgrammingPointsResourceView.GetComponent<ResourceView>();
        ppView.UpdateResourceValue(teamResource.programmingPoints);

        ResourceView mpView = SalesPointsResourceView.GetComponent<ResourceView>();
        mpView.UpdateResourceValue(teamResource.managerPoints);

        ResourceView spView = ManagerPointsResourceView.GetComponent<ResourceView>();
        spView.UpdateResourceValue(teamResource.salesPoints);

        ResourceView ipView = IdeaPointsResourceView.GetComponent<ResourceView>();
        ipView.UpdateResourceValue(teamResource.ideaPoints);

        ResourceView clientView = ClientResourceView.GetComponent<ResourceView>();
        clientView.UpdateResourceValue(audience.clients);

        ResourceView customerView = CustomerResourceView.GetComponent<ResourceView>();
        customerView.UpdateResourceValue(audience.paidClients);

        ResourceView scheduleView = ScheduleResourceView.GetComponent<ResourceView>();
        scheduleView.UpdateResourceValue(currentDate);
    }
}

using Assets.Classes;
using System;
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
    GameObject PaidClientsView;
    GameObject ScheduleResourceView;

    // Use this for initialization
    void Start () {
    }

    void LoadViews()
    {
        MoneyResourceView = gameObject.transform.Find("Money").gameObject;
        ProgrammingPointsResourceView = gameObject.transform.Find("ProgrammingPoints").gameObject;
        SalesPointsResourceView = gameObject.transform.Find("SalesPoints").gameObject;
        ManagerPointsResourceView = gameObject.transform.Find("ManagerPoints").gameObject;
        IdeaPointsResourceView = gameObject.transform.Find("IdeaPoints").gameObject;
        PaidClientsView = gameObject.transform.Find("PaidClients").gameObject;
        ClientResourceView = gameObject.transform.Find("Clients").gameObject;
        ScheduleResourceView = gameObject.transform.Find("Date").gameObject;
        loaded = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    string GetHint<T> (T value)
    {
        string valueSigned = "";
        if (long.Parse(value.ToString()) > 0)
            valueSigned = "+" + value.ToString();

        valueSigned = value.ToString();

        return String.Format("Monthly change \n\n {0}", valueSigned);
    }

    public void RedrawResources(TeamResource teamResource, TeamResource resourceMonthChanges, Audience audience, string currentDate)
    {
        if (!loaded)
            LoadViews();

        string hint;

        // resources
        ResourceView moneyView = MoneyResourceView.GetComponent<ResourceView>();
        hint = GetHint(resourceMonthChanges.money);
        moneyView.UpdateResourceValue(teamResource.money, hint);

        ResourceView ppView = ProgrammingPointsResourceView.GetComponent<ResourceView>();
        hint = GetHint(resourceMonthChanges.programmingPoints);
        ppView.UpdateResourceValue(teamResource.programmingPoints, hint);

        ResourceView mpView = ManagerPointsResourceView.GetComponent<ResourceView>();
        hint = GetHint(resourceMonthChanges.managerPoints);
        mpView.UpdateResourceValue(teamResource.managerPoints);

        ResourceView spView = SalesPointsResourceView.GetComponent<ResourceView>();
        hint = GetHint(resourceMonthChanges.salesPoints);
        spView.UpdateResourceValue(teamResource.salesPoints, hint);

        ResourceView ipView = IdeaPointsResourceView.GetComponent<ResourceView>();
        hint = GetHint(resourceMonthChanges.ideaPoints);
        ipView.UpdateResourceValue(teamResource.ideaPoints, hint);


        // audience
        ClientChangeInfo info = audience.GetMonthChange();

        ResourceView clientView = ClientResourceView.GetComponent<ResourceView>();
        hint = "";
        clientView.UpdateResourceValue(audience.clients, hint);

        ResourceView customerView = PaidClientsView.GetComponent<ResourceView>();
        customerView.UpdateResourceValue(audience.paidClients, hint);

        // date
        ResourceView scheduleView = ScheduleResourceView.GetComponent<ResourceView>();
        scheduleView.UpdateResourceValue(currentDate);
    }
}

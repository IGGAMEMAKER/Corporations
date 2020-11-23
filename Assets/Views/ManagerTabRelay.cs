using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerTabRelay : View
{
    public GameObject EmployeesTab;
    public GameObject Managers;

    public GameObject LinkToNextTeam;
    public GameObject LinkToPrevTeam;

    public GameObject EmployeeButton;
    public GameObject StatsButton;

    public GameObject StatsTab;

    // ----------------------------

    public List<GameObject> Tabs => new List<GameObject> { Managers, StatsTab };

    private void OnEnable()
    {
        ShowAll(Tabs);
        Hide(EmployeesTab);

        Show(EmployeeButton);
        Hide(StatsButton);

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();


        EmployeeButton.GetComponent<Blinker>().enabled = Teams.IsNeverHiredEmployees(Flagship);


        Draw(LinkToNextTeam, Flagship.team.Teams.Count > 1);
        Draw(LinkToPrevTeam, Flagship.team.Teams.Count > 1);
    }


    public void ShowEmployees()
    {
        Hide(StatsTab);
        Show(EmployeesTab);

        Show(StatsButton);
        Hide(EmployeeButton);
    }

    public void ShowStats()
    {
        Show(StatsTab);
        Hide(EmployeesTab);

        Show(EmployeeButton);
        Hide(StatsButton);
    }




    //void ClearEvents(GameEntity eventContainer, List<GameEventType> removableEvents)
    //{
    //    var events = eventContainer.gameEventContainer.Events;

    //    events.RemoveAll(e => removableEvents.Contains(e.eventType));
    //    eventContainer.ReplaceGameEventContainer(events);
    //}

    //void MarkGameEventsAsSeen(WorkerRole role)
    //{
    //    var marketingEvents = new List<GameEventType> { GameEventType.NewMarketingChannel };

    //    var events = NotificationUtils.GetGameEventContainerEntity(Q);

    //    if (role == WorkerRole.MarketingLead)
    //    {
    //        ClearEvents(events, marketingEvents);
    //    }
    //}
}

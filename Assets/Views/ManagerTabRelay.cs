﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTabRelay : View
{
    public GameObject EmployeesTab;
    public GameObject HireManagersRelayButtons;
    public GameObject Managers;
    public GameObject ManagerInteractions;

    public GameObject BackToManagerTab;
    public GameObject BackToMainTab;

    public List<GameObject> Tabs => new List<GameObject> { EmployeesTab, HireManagersRelayButtons, ManagerInteractions, Managers, BackToMainTab, BackToManagerTab };

    // selected worker
    bool roleWasSelected = false;
    WorkerRole SelectedWorkerRole;

    public bool IsRoleChosen(WorkerRole workerRole)
    {
        return roleWasSelected && SelectedWorkerRole == workerRole;
    }

    public void ToggleRole(WorkerRole role)
    {
        if (role == SelectedWorkerRole)
        {
            // toggling role
            roleWasSelected = !roleWasSelected;
        }
        else
        {
            // click on different role
            roleWasSelected = true;
            SelectedWorkerRole = role;

            // TODO unnecessary?
            //var up = CompanyUpgrades.GetComponent<ProductUpgradeButtons>();
            //up.WorkerRole = role;
            //up.ViewRender();
        }

        // enabled
        if (roleWasSelected)
        {
            FindObjectOfType<WorkerHierarchyListView>().ViewRender();
            OpenWorkerTab();
            ScheduleUtils.PauseGame(Q);

        }
        else
        {
            OpenManagerTab();
        }

        MarkGameEventsAsSeen(role);
    }


    void ClearEvents(GameEntity eventContainer, List<GameEventType> removableEvents)
    {
        var events = eventContainer.gameEventContainer.Events;

        events.RemoveAll(e => removableEvents.Contains(e.eventType));
        eventContainer.ReplaceGameEventContainer(events);
    }

    void MarkGameEventsAsSeen(WorkerRole role)
    {
        var marketingEvents = new List<GameEventType> { GameEventType.NewMarketingChannel };

        var events = NotificationUtils.GetGameEventContainerEntity(Q);

        if (role == WorkerRole.MarketingLead)
        {
            ClearEvents(events, marketingEvents);
        }
    }

    public void HireWorker(WorkerRole workerRole)
    {
        ShowOnly(EmployeesTab, Tabs);
        Show(BackToManagerTab);

        var candidates = FindObjectOfType<CandidatesForRoleListView>();
        candidates.WorkerRole = workerRole;
        candidates.ViewRender();
    }

    // Open managerS tab
    public void OpenManagerTab()
    {
        ShowOnly(Managers, Tabs);
        Show(HireManagersRelayButtons);
        Show(BackToMainTab);
    }

    public void OpenWorkerTab()
    {
        ShowOnly(ManagerInteractions, Tabs);
        Show(Managers);
        Show(BackToMainTab);
    }

    private void OnEnable()
    {
        OpenManagerTab();
    }
}

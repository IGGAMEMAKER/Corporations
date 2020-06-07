using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    bool roleWasSelected = false;
    WorkerRole SelectedWorkerRole;

    public GameObject CompanyUpgrades;
    public GameObject MarketingCampaigns;

    FlagshipRelayInCompanyView flagshipRelay;

    public EnlargeOnDemand EnlargeOnDemand;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        bool highlightRole = !roleWasSelected || (roleWasSelected && role == SelectedWorkerRole);
        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            SetItems(roles);
        }
    }

    public bool IsRoleChosen(WorkerRole workerRole)
    {
        return roleWasSelected && SelectedWorkerRole == workerRole;
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        flagshipRelay = FindObjectOfType<FlagshipRelayInCompanyView>();

        ViewRender();
    }

    void HighlightManagers()
    {
        foreach (Transform child in transform)
        {
            var c = child.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>();

            var role = c.role;

            bool thisExactRoleWasSelected = roleWasSelected && role == SelectedWorkerRole;

            bool highlightRole = !roleWasSelected || thisExactRoleWasSelected;

            c.HighlightWorkerRole(highlightRole);
        }
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
            var up = CompanyUpgrades.GetComponent<ProductUpgradeButtons>();
            up.WorkerRole = role;
            up.ViewRender();
        }

        // enabled
        if (roleWasSelected)
        {
            flagshipRelay.ChooseWorkerInteractions();
        }
        else
        {
            flagshipRelay.ChooseDevTab();
        }

        MarkGameEventsAsSeen(role);

        //EnlargeOnDemand.StartAnimation();

        HighlightManagers();
    }

    private void OnDisable()
    {
        roleWasSelected = false;
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
}

using Assets.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTabRelay : View
{
    public GameObject EmployeesTab;
    public GameObject Managers;

    public GameObject LinkToNextTeam;
    public GameObject LinkToPrevTeam;

    public GameObject EmployeeButton;
    public GameObject StatsButton;
    public GameObject MergeTeamsButton;
    public GameObject ManagersButton;
    
    public GameObject DependantTeams;
    public GameObject MergeCandidates;

    public GameObject StatsTab;

    [Header("Team type image")]
    public Image TeamImage;
    public Text TeamRankValue;

    [Header("Team type sprites")]
    public Sprite UniversalTeamSprite;
    public Sprite UnknownTeamSprite;
    public Sprite MarketingTeamSprite;
    public Sprite ServersideTeamSprite;
    public Sprite DevTeamSprite;

    // ----------------------------

    public List<GameObject> Tabs => new List<GameObject> { Managers, StatsTab };
    public List<GameObject> MergeCandidatesTab => new List<GameObject> { MergeCandidates };
    
    public List<GameObject> TeamsTabs => new List<GameObject> { MergeCandidates, DependantTeams };
    
    public List<GameObject> TeamButtons => new List<GameObject> { EmployeeButton, StatsButton, MergeTeamsButton };
    
    public List<GameObject> EmployeeStuff => new List<GameObject> { EmployeeButton, EmployeesTab };
    public List<GameObject> StatsStuff => new List<GameObject> { StatsButton, StatsTab };
    
    

    private void OnEnable()
    {
        ShowAll(Tabs);
        
        ShowEmployees();

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();


        EmployeeButton.GetComponent<Blinker>().enabled = Teams.IsNeverHiredEmployees(Flagship);
        Draw(MergeTeamsButton,  Teams.IsCanReceiveTeams(Flagship.team.Teams[SelectedTeam]));

        RenderTeamRank();

        Draw(LinkToNextTeam, Flagship.team.Teams.Count > 1);
        Draw(LinkToPrevTeam, Flagship.team.Teams.Count > 1);
    }

    public void ShowManagers()
    {
        Hide(ManagersButton);
        
        ShowAll(Tabs);
    }
    
    public void ShowEmployees()
    {
        ShowOnly(StatsButton, StatsStuff);
        ShowOnly(EmployeesTab, EmployeeStuff);
        
        HideMergingCandidates();
    }

    public void ShowStats()
    {
        ShowOnly(StatsTab, StatsStuff);
        ShowOnly(EmployeeButton, EmployeeStuff);
        
        HideMergingCandidates();
    }

    public void ShowMergingTeams()
    {
        ShowAll(MergeCandidatesTab);
        HideAll(Tabs);
        
        Hide(MergeTeamsButton);
        Show(StatsButton);
    }

    void HideMergingCandidates()
    {
        Show(MergeTeamsButton);
        
        HideAll(MergeCandidatesTab);
    }

    void RenderTeamRank()
    {
        var team = Flagship.team.Teams[SelectedTeam];

        TeamRankValue.text = (int)team.Rank + "";

        switch (team.TeamType)
        {
            case TeamType.CrossfunctionalTeam: TeamImage.sprite = UniversalTeamSprite; break;
            case TeamType.MarketingTeam: TeamImage.sprite = MarketingTeamSprite; break;
            case TeamType.DevelopmentTeam: TeamImage.sprite = DevTeamSprite; break;
            case TeamType.ServersideTeam: TeamImage.sprite = ServersideTeamSprite; break;

            default: TeamImage.sprite = UnknownTeamSprite; break;
        }
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

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
    
    public GameObject DependantTeams;
    public GameObject MergeCandidates;
    public GameObject YouCanMergeTeamsLabel;

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
    public List<GameObject> MergeCandidatesTab => new List<GameObject> { MergeCandidates, YouCanMergeTeamsLabel };

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

        RenderTeamRank();

        Draw(LinkToNextTeam, Flagship.team.Teams.Count > 1);
        Draw(LinkToPrevTeam, Flagship.team.Teams.Count > 1);
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

    public void ShowMergingTeams()
    {
        ShowAll(MergeCandidatesTab);
        HideAll(Tabs);
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

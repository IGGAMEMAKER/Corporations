using Assets.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTabRelay : View
{
    public GameObject EmployeesTab;
    public GameObject Managers;
    public GameObject TeamRelations;

    public GameObject LinkToNextTeam;
    public GameObject LinkToPrevTeam;

    public GameObject MergeTeamsButton;
    public GameObject ManagersButton;
    
    public GameObject MergeCandidates;

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

    // public List<GameObject> Tabs => new List<GameObject> { Managers };
    // public List<GameObject> MergeCandidatesTab => new List<GameObject> { MergeCandidates };
    
    // public List<GameObject> EmployeeStuff => new List<GameObject> { EmployeeButton, EmployeesTab };
    

    private void OnEnable()
    {
        ShowMainScreen();
        
        ViewRender();
    }


    public override void ViewRender()
    {
        base.ViewRender();


        // EmployeeButton.GetComponent<Blinker>().enabled = Teams.IsNeverHiredEmployees(Flagship);

        RenderTeamRank();

        Draw(LinkToNextTeam, Flagship.team.Teams.Count > 1);
        Draw(LinkToPrevTeam, Flagship.team.Teams.Count > 1);
    }

    public void OnShowMainScreen()
    {
        ShowMainScreen();
    }
    
    public void OnShowMergingTeams()
    {
        ShowMergingTeams();
    }
    
    void ShowMainScreen()
    {
        ShowAll(Managers, EmployeesTab, TeamRelations);
        ConditionalShowMergeTeamsButton();
        
        HideAll(MergeCandidates);
        
        Hide(ManagersButton);
    }

    void ShowMergingTeams()
    {
        HideAll(Managers, EmployeesTab, TeamRelations);

        ShowAll(MergeCandidates);
        
        Hide(MergeTeamsButton);
        Show(ManagersButton);
    }

    void ConditionalShowMergeTeamsButton()
    {
        bool needsMergeButton = Teams.IsHasMergeCandidates(Flagship.team.Teams[SelectedTeam], Flagship); 

        Draw(MergeTeamsButton, needsMergeButton);
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

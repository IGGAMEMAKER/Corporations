using System.Collections.Generic;
using UnityEngine;

public class TeamTabButtonsView : View
{
    public GameObject NewTeams;
    public GameObject BackToCompanyManagement;
    public GameObject CrossTeam;
    public GameObject DevTeam;
    public GameObject MarketingTeam;
    public GameObject CorporateCulture;
    public GameObject Hierarchy;

    public IEnumerable<GameObject> TeamHiringButtons => new[] {CrossTeam, DevTeam, MarketingTeam, BackToCompanyManagement};
    public IEnumerable<GameObject> MainTeamButtons => new[] {CorporateCulture, Hierarchy, NewTeams};

    private void OnEnable()
    {
        OnBackToMainTeamButtons();
    }

    public void OnBackToMainTeamButtons()
    {
        HideAll(TeamHiringButtons);
        ShowAll(MainTeamButtons);

        Hide(Hierarchy);
    }
    
    public void OnShowTeamActionsOnly()
    {
        ShowAll(TeamHiringButtons);
        HideAll(MainTeamButtons);

        Hide(Hierarchy);
    }
}

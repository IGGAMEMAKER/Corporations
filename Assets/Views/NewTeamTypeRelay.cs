using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTeamTypeRelay : View
{
    public GameObject UniversalTeam;

    public GameObject DevelopmentTeam;
    public GameObject MarketingTeam;
    public GameObject SupportTeam;
    public GameObject ServersTeam;

    public void SetTeamTypes()
    {
        bool canExploreSpecialisedTeams = Flagship.team.Teams.Count > 2;

        Draw(UniversalTeam, true);

        Draw(DevelopmentTeam, canExploreSpecialisedTeams);
        Draw(MarketingTeam, canExploreSpecialisedTeams);
        Draw(SupportTeam, canExploreSpecialisedTeams);
        Draw(ServersTeam, canExploreSpecialisedTeams);
    }
}

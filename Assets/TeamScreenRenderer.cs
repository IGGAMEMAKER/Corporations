using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScreenRenderer : MonoBehaviour
{
    GameObject Workers;
    GameObject TeamMorale;
    public void RenderTeam(Project p)
    {
        Team team = p.Team;
        Workers = gameObject.transform.Find("Workers").gameObject;
        Workers.GetComponent<WorkerListRenderer>().UpdateList(team.Workers);

        TeamMorale = gameObject.transform.Find("TeamMorale").gameObject;
        TeamMorale.GetComponent<TeamMoraleView>().Redraw(p.moraleData);
    }
}

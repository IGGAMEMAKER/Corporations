using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScreenRenderer : MonoBehaviour
{
    GameObject Workers;
    GameObject TeamMorale;
    public void RenderTeam(Team team)
    {
        Workers = gameObject.transform.Find("Workers").gameObject;
        Workers.GetComponent<WorkerListRenderer>().UpdateList(team.Workers);

        TeamMorale = gameObject.transform.Find("Morale").gameObject;
        TeamMorale.GetComponent<TeamMoraleView>().Redraw(team.moraleData);
    }
}

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
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["teamMorale"] = p.moraleData.Morale;

        Workers = gameObject.transform.Find("Workers").gameObject;
        Workers.GetComponent<WorkerListRenderer>().UpdateList(team.Workers, parameters);

        TeamMorale = gameObject.transform.Find("TeamMorale").gameObject;
        TeamMorale.GetComponent<TeamMoraleView>().Redraw(p.moraleData);
    }
}

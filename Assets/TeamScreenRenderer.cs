using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScreenRenderer : MonoBehaviour
{
    GameObject screen;
    public void RenderTeam(Team team)
    {
        screen = gameObject.transform.Find("TeamRenderer").gameObject;

        screen.GetComponent<WorkerListRenderer>().UpdateList(team.Workers);
    }
}

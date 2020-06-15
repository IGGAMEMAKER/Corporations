using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public Text TeamName;
    public TeamType TeamType;
    public GameObject RemoveTeam;

    public AddTeamTaskListView AddTeamTaskListView;
    public TeamTaskListView TeamTaskListView;

    public void SetEntity(TeamType teamType, int teamId)
    {
        TeamType = teamType;

        RemoveTeam.GetComponent<RemoveTeamController>().TeamType = teamType;
        TeamName.text = TeamType.ToString() + " " + teamId;

        var max = 5;

        var freeSlots = Random.Range(0, max);
        var chosenSlots = max - freeSlots;

        AddTeamTaskListView.FreeSlots = freeSlots;
        AddTeamTaskListView.SetEntity(teamId);

        TeamTaskListView.ChosenSlots = chosenSlots;
        TeamTaskListView.SetEntity(teamId);
    }
}

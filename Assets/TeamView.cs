using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public Text TeamName;
    public TeamType TeamType;
    public GameObject RemoveTeam;

    public void SetEntity(TeamType teamType, int index)
    {
        TeamType = teamType;

        RemoveTeam.GetComponent<RemoveTeamController>().TeamType = teamType;
        TeamName.text = TeamType.ToString() + " " + index;
    }
}

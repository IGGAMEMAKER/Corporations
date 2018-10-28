using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMoraleView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Redraw(TeamMoraleData moraleData)
    {
        Debug.Log("RedrawTeamMorale");

        GameObject ProgressBar = gameObject.transform.Find("ProgressBar").gameObject;
        ProgressBar.GetComponent<ProgressBar>().SetValue(moraleData.Morale);

        GameObject LoyaltyHint = ProgressBar.transform.Find("Hint").gameObject;
        LoyaltyHint.GetComponent<UIHint>().SetHintObject(moraleData.Morale + "");


        GameObject MoraleDescription = gameObject.transform.Find("TeamMoraleStatus").gameObject;
        MoraleDescription.GetComponent<Text>().text = "Morale: " + moraleData.Morale;

        GameObject MoraleHint = MoraleDescription.transform.Find("Hint").gameObject;

        string moraleHint = String.Format(
            "Is team: {0}  {1}",
            moraleData.isTeam
            );

        MoraleHint.GetComponent<UIHint>().SetHintObject(moraleHint);
    }
}

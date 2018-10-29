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

    string DescribeMoraleBonus(bool isBonus, int bonus)
    {
        if (isBonus)
            return "YES   +" + bonus;

        return "NO";
    }

    internal void Redraw(TeamMoraleData moraleData)
    {
        GameObject ProgressBar = gameObject.transform.Find("ProgressBar").gameObject;
        ProgressBar.GetComponent<ProgressBar>().SetValue(moraleData.Morale);

        //GameObject LoyaltyHint = ProgressBar.transform.Find("Hint").gameObject;
        //LoyaltyHint.GetComponent<UIHint>().SetHintObject(moraleData.Morale + "");

        GameObject MoraleDescription = gameObject.transform.Find("TeamMoraleStatus").gameObject;
        MoraleDescription.GetComponent<Text>().text = "Morale: " + moraleData.Morale;

        GameObject MoraleHint = MoraleDescription.transform.Find("Hint").gameObject;

        string moraleHint = String.Format(
            "Base value: {4}\n" +
            "Is team: {0}\n" +
            "Is making money: {1}\n" +
            "Is innovative: {2}\n" +
            "Is top company: {3}\n" +
            "Salaries: {5}\n" +
            "Team Size: -{6}",
            DescribeMoraleBonus(moraleData.isTeam, Balance.MORALE_BONUS_IS_TEAM),
            DescribeMoraleBonus(moraleData.isMakingMoney, Balance.MORALE_BONUS_IS_PROFITABLE),
            DescribeMoraleBonus(moraleData.isInnovative, Balance.MORALE_BONUS_IS_INNOVATIVE),
            DescribeMoraleBonus(moraleData.isTopCompany, Balance.MORALE_BONUS_IS_PRESTIGEOUS_COMPANY),
            "+" + Balance.MORALE_BONUS_BASE,
            "+" + moraleData.salaries,
            moraleData.teamSizePenalty
        );

        MoraleHint.GetComponent<UIHint>().SetHintObject(moraleHint);
        //LoyaltyHint.GetComponent<UIHint>().SetHintObject(moraleHint);
    }
}

using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamMoraleView : MonoBehaviour {
    string DescribeMoraleBonus(bool hasBonus, int bonus)
    {
        if (hasBonus)
            return "YES   +" + bonus;

        return "NO";
    }

    internal void Redraw(TeamMoraleData moraleData)
    {
        GameObject MoraleDescription = gameObject.transform.Find("TeamMoraleStatus").gameObject;
        MoraleDescription.GetComponent<Text>().text = "Team Morale: " + moraleData.Morale;

        GameObject MoraleHint = MoraleDescription.transform.Find("Hint").gameObject;

        string moraleHint = String.Format(
            "Shows team's desire to continue working in your company\n\n" +
            "Base value: {4}\n" +
            "Salaries: {5}\n" +
            "Team Size: {6}\n\n" +
            "Is team: {0}\n" +
            "Is making money: {1}\n" +
            "Is innovative: {2}\n" +
            "Is top company: {3}\n",
            DescribeMoraleBonus(moraleData.isTeam, Balance.MORALE_BONUS_IS_TEAM),
            DescribeMoraleBonus(moraleData.isMakingMoney, Balance.MORALE_BONUS_IS_PROFITABLE),
            DescribeMoraleBonus(moraleData.isInnovative, Balance.MORALE_BONUS_IS_INNOVATIVE),
            DescribeMoraleBonus(moraleData.isTopCompany, Balance.MORALE_BONUS_IS_PRESTIGEOUS_COMPANY),
            "+" + Balance.MORALE_BONUS_BASE,
            "+" + moraleData.salaries,
            moraleData.teamSizePenalty
        );

        MoraleHint.GetComponent<UIHint>().SetHintObject(moraleHint);
    }
}

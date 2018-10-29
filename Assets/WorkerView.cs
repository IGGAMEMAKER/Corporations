using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RenderMorale (int morale)
    {
        GameObject MoraleValue = gameObject.transform.Find("MoraleValue").gameObject;
        Text component = MoraleValue.GetComponent<Text>();

        string text;

        if (morale > 30)
        {
            text = "High";
            component.color = Color.green;
        } else if (morale > 0)
        {
            text = "Normal";
            component.color = Color.gray;
        } else
        {
            text = "Terrible";
            component.color = Color.red;
        }

        component.text = text + " (" + morale + ")";

        
        GameObject MoraleBar = gameObject.transform.Find("ProgressBar").gameObject;

        // hide progressBar if morale is negative
        if (morale < 0)
            MoraleBar.SetActive(true);
        else
            MoraleBar.SetActive(false);
    }

    string GetSignedValue (int value)
    {
        if (value > 0)
            return "+" + value;

        return "" + value;
    }

    void RedrawMoraleHint(Human human, int teamMorale)
    {
        GameObject MoraleValue = gameObject.transform.Find("MoraleValue").gameObject;
        UIHint MoraleHint = MoraleValue.GetComponentInChildren<UIHint>();

        string hintText = String.Format(
            "Base Morale: {1} \n"+
            "Team Morale: {0}",
            GetSignedValue(teamMorale),
            Balance.MORALE_PERSONAL_BASE
        );

        MoraleHint.SetHintObject(hintText);
    }

    void ShowSkills(Human human)
    {
        GameObject Avatar = gameObject.transform.Find("Name").gameObject;
        UIHint SkillsetHint = Avatar.GetComponentInChildren<UIHint>();

        string hintText = String.Format(
            "          {3}         \n\n" +
            "<b>Management</b>  - {0} LVL \n" +
            "<b>Programming</b> - {1} LVL \n" +
            "<b>Marketing</b>   - {2} LVL \n",
            human.Skills.Management.Level,
            human.Skills.Programming.Level,
            human.Skills.Marketing.Level,
            human.GetLiteralSpecialisation()
        );

        SkillsetHint.SetHintObject(hintText);
    }

    public void UpdateView(Human human, int index, Dictionary<string, object> parameters)
    {
        GameObject NameObject = gameObject.transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName;

        int morale = (int)parameters["teamMorale"] + Balance.MORALE_PERSONAL_BASE;

        RenderMorale(morale);

        RedrawMoraleHint(human, (int)parameters["teamMorale"]);

        ShowSkills(human);

        GameObject Effeciency = gameObject.transform.Find("Effeciency").gameObject;
        Effeciency.GetComponent<Text>().text = String.Format("+{0} points monthly", human.BaseProduction);
    }
}

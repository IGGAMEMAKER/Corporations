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

    public void UpdateView(Human human, int index)
    {
        GameObject NameObject = gameObject.transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName;

        GameObject MoraleBar = gameObject.transform.Find("ProgressBar").gameObject;
        MoraleBar.GetComponent<ProgressBar>().SetValue(human.Morale);

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

        GameObject Effeciency = gameObject.transform.Find("Effeciency").gameObject;
        Effeciency.GetComponent<Text>().text = String.Format("+{0} points monthly", human.BaseProduction);
    }
}

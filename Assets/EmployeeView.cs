using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EmployeeView : MonoBehaviour {
    string GetSignedValue(int value)
    {
        if (value > 0)
            return "+" + value;

        return "" + value;
    }

    void RenderSkills(Human human)
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

    void RenderEffeciency(Human human)
    {
        GameObject Effeciency = gameObject.transform.Find("Effeciency").gameObject;
        Effeciency.GetComponent<Text>().text = String.Format("+{0} points monthly", human.BaseProduction);
    }

    void RenderLevel(Human human)
    {
        Text text = gameObject.transform.Find("Avatar").GetComponentInChildren<Text>();
        text.text = human.Level.ToString();
    }

    void RenderName(Human human)
    {
        GameObject NameObject = gameObject.transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName + " \n " + human.Level + "lvl";
    }

    void RenderHireButton(int workerId, int projectId)
    {
        Button button = gameObject.transform.Find("Hire").gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate { BaseController.HireWorker(workerId, projectId); });
    }

    public void UpdateView(Human human, int index, Dictionary<string, object> parameters)
    {
        RenderName(human);
        RenderLevel(human);
        RenderSkills(human);
        RenderEffeciency(human);

        int projectId = (int)parameters["projectId"];
        RenderHireButton(index, projectId);
    }
}

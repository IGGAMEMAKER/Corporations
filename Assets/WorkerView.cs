using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour {
    public ProgressBar MoraleProgressBar;
    public UIHint MoraleProgressHint;

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
    }

    void RenderMoraleProgressBar(Human human, int workerMorale, int teamMorale)
    {
        //GameObject MoraleBar = gameObject.transform.Find("ProgressBar").gameObject;

        // hide progressBar if morale is negative
        MoraleProgressBar.enabled = workerMorale < 0;
        //MoraleBar.SetActive(workerMorale < 0);

        //ProgressBar progressBar = MoraleBar.GetComponent<ProgressBar>();
        MoraleProgressBar.SetValue(human.DesireToLeave);

        int moraleChange = human.GetMoraleChange(teamMorale);

        int monthsToDemoralize = moraleChange < 0 ? (Balance.MORALE_PERSONAL_MAX - human.DesireToLeave) / moraleChange : 100000;

        string hint = String.Format("Desire to leave increases by {0} each month" +
            "\nThis worker will stop working" +
            "\nin {1} months", moraleChange, monthsToDemoralize);

        //MoraleProgressBar.gameObject.GetComponentInChildren<UIHint>().SetHintObject(hint);
        MoraleProgressHint.SetHintObject(hint);
    }

    string GetSignedValue (int value)
    {
        if (value > 0)
            return "+" + value;

        return "" + value;
    }

    void RedrawMoraleHint(Human human, int teamMorale)
    {
        GameObject MoraleValue = transform.Find("MoraleValue").gameObject;
        UIHint MoraleHint = MoraleValue.GetComponentInChildren<UIHint>();

        string hintText = String.Format(
            "Base Morale: {0} \n\n"+
            "Team Morale: {1} \n"+
            "Individual Morale: {2} \n",
            Balance.MORALE_PERSONAL_BASE,
            GetSignedValue(teamMorale),
            GetSignedValue(human.BaseLoyalty)
        );

        MoraleHint.SetHintObject(hintText);
    }

    void RenderSkills(Human human)
    {
        GameObject Avatar = transform.Find("Name").gameObject;
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
        GameObject Effeciency = transform.Find("Effeciency").gameObject;

        string text;
        if (human.IsCompletelyDemoralised)
            text = "STOPPED WORKING";
        else
            text = String.Format("+{0} points monthly", human.BaseProduction);

        Effeciency.GetComponent<Text>().text = text;
    }

    void RenderAvatar(Human human)
    {
        var avatar = transform.Find("Avatar").GetComponentInChildren<WorkerAvatarView>();
        avatar.SetAvatar(human.Level, human.Specialisation);
    }

    void RenderName(Human human)
    {
        GameObject NameObject = transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName;
    }

    void RenderHireButton(int workerId, int projectId)
    {
        Button button = transform.Find("Fire").gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate { BaseController.FireWorker(workerId, projectId); });
    }

    private void RenderSkillProgression(Human human)
    {
        GameObject progressBar = gameObject.transform.Find("SkillProgressBar").gameObject;

        if (human.Level == Balance.SKILL_MAX_LEVEL)
            progressBar.SetActive(false);

        progressBar.GetComponent<ProgressBar>().SetValue(human.SpecialisationSkill.ProgressToNextLevel);

        string hint = String.Format("     Progress {0}%\n\n" +
            "need {1} more XP to levelup \n\n" +
            "Workers get XP when they:\n" +
            "\t * upgrade features\n" +
            "\t * make ad campaigns\n" +
            "\t * e.t.c.",
            human.SpecialisationSkill.ProgressToNextLevel,
            human.SpecialisationSkill.RequiredXP
            );

        UIHint uIHint = progressBar.GetComponentInChildren<UIHint>();
        uIHint.SetHintObject(hint);
        uIHint.Rotate(-90);
    }

    public void UpdateView(Human human, int index, Dictionary<string, object> parameters)
    {
        RenderName(human);
        RenderAvatar(human);

        RenderSkillProgression(human);

        RenderSkills(human);
        RenderEffeciency(human);

        int projectId = (int)parameters["projectId"];

        RenderHireButton(index, projectId);

        int teamMorale = (int)parameters["teamMorale"];

        int workerMorale = teamMorale + Balance.MORALE_PERSONAL_BASE + human.BaseLoyalty;

        RenderMorale(workerMorale);
        RedrawMoraleHint(human, teamMorale);

        RenderMoraleProgressBar(human, workerMorale, teamMorale);
    }
}

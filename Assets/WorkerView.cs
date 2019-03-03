using Assets.Classes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour {
    public ProgressBar DemoralisationProgressBar;
    public Hint DemoralisationHint;

    public Text MoraleText;
    public Hint MoraleHint;
    public Hint SkillsetHint;

    public Hint SkillProgressionHint;

    public Hint FireWorkerHint;

    void RenderMorale (int morale)
    {
        string text;

        if (morale > 30)
        {
            text = "High";
            MoraleText.color = Color.green;
        } else if (morale > 0)
        {
            text = "Normal";
            MoraleText.color = Color.gray;
        } else
        {
            text = "Terrible";
            MoraleText.color = Color.red;
        }

        MoraleText.text = text + " (" + morale + ")";
    }

    void RenderMoraleProgressBar(Human human, int workerMorale, int teamMorale)
    {
        // hide progressBar if morale is negative
        DemoralisationProgressBar.gameObject.SetActive(workerMorale < 0);
        DemoralisationProgressBar.SetValue(human.DesireToLeave);

        int moraleChange = human.GetMoraleChange(teamMorale);

        int monthsToDemoralize = moraleChange < 0 ? (Balance.MORALE_PERSONAL_MAX - human.DesireToLeave) / moraleChange : 100000;

        string hint = String.Format("Desire to leave increases by {0} each month" +
            "\nThis worker will stop working" +
            "\nin {1} months", moraleChange, monthsToDemoralize);

        DemoralisationHint.SetHintObject(hint);
    }

    string GetSignedValue (int value)
    {
        if (value > 0)
            return "+" + value;

        return "" + value;
    }

    void RedrawMoraleHint(Human human, int teamMorale)
    {
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

    string GetEffeciency(Human human)
    {
        if (human.IsCompletelyDemoralised)
            return "STOPPED WORKING";

        return String.Format("+{0} points monthly", human.BaseProduction);
    }

    void RenderEffeciency(Human human)
    {
        GameObject Effeciency = transform.Find("Effeciency").gameObject;

        Effeciency.GetComponent<Text>().text = GetEffeciency(human);
    }

    void RenderAvatar(Human human)
    {
        var avatar = transform.Find("Avatar").GetComponentInChildren<WorkerAvatarView>();
        avatar.Render(human.FullName, human.Level, human.Specialisation);
    }

    void RenderName(Human human)
    {
        GameObject NameObject = transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName;
    }

    void RenderFireHint(Human human)
    {
        FireWorkerHint.SetHintObject("Fire " + human.FullName + "!");
    }

    void RenderHireButton(int workerId, int projectId)
    {
        Button button = transform.Find("Fire").gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();

        //button.onClick.AddListener(delegate { EventManager.FireWorker(workerId, projectId); });
    }

    private void RenderSkillProgression(Human human)
    {
        GameObject progressBar = gameObject.transform.Find("SkillProgressBar").gameObject;

        if (human.Level == Balance.SKILL_MAX_LEVEL)
            progressBar.SetActive(false);

        progressBar.GetComponent<ProgressBar>().SetValue(human.SpecialisationSkill.ProgressToNextLevel);

        string hint = String.Format(
            "     Progress {0}%\n\n" +
            "need {1} more XP to levelup \n\n" +
            "Workers get XP when they:\n" +
            "\t * upgrade features\n" +
            "\t * make ad campaigns\n" +
            "\t * e.t.c.",
            human.SpecialisationSkill.ProgressToNextLevel,
            human.SpecialisationSkill.RequiredXP
            );

        SkillProgressionHint.SetHintObject(hint);
    }

    public void Render(Human human, int index, int projectId, int teamMorale)
    {
        RenderName(human);
        RenderAvatar(human);

        RenderSkillProgression(human);

        RenderSkills(human);
        RenderEffeciency(human);


        RenderHireButton(index, projectId);

        RenderFireHint(human);


        int workerMorale = teamMorale + Balance.MORALE_PERSONAL_BASE + human.BaseLoyalty;

        RenderMorale(workerMorale);
        RedrawMoraleHint(human, teamMorale);

        RenderMoraleProgressBar(human, workerMorale, teamMorale);
    }

    public void UpdateView(Human human, int index, Dictionary<string, object> parameters)
    {
        int projectId = (int)parameters["projectId"];
        int teamMorale = (int)parameters["teamMorale"];

        Render(human, index, projectId, teamMorale);
    }
}

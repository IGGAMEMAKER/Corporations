using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionRelay : View
{
    public GameObject ActiveMissions;
    public GameObject NewMissions;

    public GameObject GoalsListView;
    public GameObject PickGoalsListView;

    void RenderButtons()
    {
        var myGoals = MyCompany.companyGoal.Goals.Count;
        var newGoals = Investments.GetNewGoals(MyCompany, Q).Count;

        ActiveMissions.GetComponentInChildren<TextMeshProUGUI>().text = $"ACTIVE ({myGoals})";
        NewMissions.GetComponentInChildren<TextMeshProUGUI>().text = $"NEW ({newGoals})";
    }

    private void OnEnable()
    {
        ShowActiveMissions();
    }

    public void ShowActiveMissions()
    {
        Show(GoalsListView);
        Hide(PickGoalsListView);

        RenderButtons();
    }

    public void ShowNewMissions()
    {
        Hide(GoalsListView);
        Show(PickGoalsListView);

        RenderButtons();
    }
}

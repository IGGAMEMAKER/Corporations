using Assets.Core;
using UnityEngine;
using TMPro;

public class MissionRelay : View
{
    public GameObject ActiveMissions;
    public GameObject NewMissions;

    public GameObject GoalsListView;
    public GameObject PickGoalsListView;

    int myGoalsCounter => MyCompany.companyGoal.Goals.Count;
    int newGoalsCounter => Investments.GetNewGoals(MyCompany, Q).Count;

    public void RenderButtons(bool focusOnActive)
    {
        var myGoals = myGoalsCounter;
        var newGoals = newGoalsCounter;

        var missionsText = ActiveMissions.GetComponentInChildren<TextMeshProUGUI>();
        missionsText.text = $"ACTIVE ({myGoalsCounter})";
        missionsText.color = Visuals.GetColorFromString(focusOnActive ? Colors.COLOR_GOLD : Colors.COLOR_NEUTRAL);

        var newMissionsText = NewMissions.GetComponentInChildren<TextMeshProUGUI>();
        newMissionsText.text = $"NEW ({newGoalsCounter})";
        newMissionsText.color = Visuals.GetColorFromString(!focusOnActive ? Colors.COLOR_GOLD : Colors.COLOR_NEUTRAL);

        Draw(ActiveMissions, myGoals > 0);
        Draw(NewMissions, newGoals > 0);
    }

    private void OnEnable()
    {
        if (myGoalsCounter != 0)
            ShowActiveMissions();
        else
            ShowNewMissions();
    }

    public void ShowActiveMissions()
    {
        Show(GoalsListView);
        Hide(PickGoalsListView);

        RenderButtons(true);
    }

    public void ShowNewMissions()
    {
        Hide(GoalsListView);
        Show(PickGoalsListView);

        RenderButtons(false);
    }
}

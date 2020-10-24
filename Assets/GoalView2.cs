using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView2 : View
{
    public Text Title;
    public Text Requirements;
    public Text Number;
    public GameObject CompleteButton;

    InvestmentGoal InvestmentGoal;
    int index;

    public void SetEntity(InvestmentGoal goal, int index)
    {
        InvestmentGoal = goal;
        this.index = index;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        bool completed = InvestmentGoal.IsCompleted(Flagship, Q);

        Draw(CompleteButton, completed);
        Draw(Requirements, !completed);

        Title.text = InvestmentGoal.GetFormattedName();
        Requirements.text = InvestmentGoal.GetFormattedRequirements(Flagship, Q);
        Number.text = $"#{index + 1}";
    }

    public void CompleteGoal()
    {
        Investments.CompleteGoal(Flagship, Q, InvestmentGoal);
        SoundManager.Play(Sound.Action);
        FindObjectOfType<GoalsListView>().ViewRender();
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : View
{
    public Text Title;
    public Text Description;
    public ProgressBar ProgressBar;
    public GameObject Users;

    public GameObject CompetitionPanel;

    public override void ViewRender()
    {
        base.ViewRender();

        var clients = Marketing.GetUsers(Flagship);
        var goal = Flagship.companyGoal.InvestorGoal;

        var requirements = Investments.GetGoalRequirements(Flagship, Q);
        var req = requirements[0];

        switch (goal)
        {
            case InvestorGoalType.Prototype:
                SetPanel("Make test audience loyal", req, "Loyalty");
                break;

            case InvestorGoalType.BecomeMarketFit:
                SetPanel("Make audience extremely loyal", req, "Loyalty");
                break;

            case InvestorGoalType.FirstUsers:
                SetPanel($"Accumulate {Format.Minify(req.need)} users", req, "Users");
                break;

            case InvestorGoalType.Release:
                SetPanel("Release your product!", req, "Is not released");
                break;

            case InvestorGoalType.BecomeProfitable:
            case InvestorGoalType.Operationing:
                //SetPanel("Increase your income", req, $"Income from product");
                ShowCompetitionPanel();
                break;

            default:
                SetPanel("Default goal", req, goal.ToString());
                break;
        }
    }

    void ShowGoalPanel()
    {
        Show(Description);
        Show(ProgressBar);
        Show(Title);
        Show(Users);
        Hide(CompetitionPanel);
    }

    void ShowCompetitionPanel()
    {
        Hide(Description);
        Hide(ProgressBar);
        Hide(Title);
        Hide(Users);
        Show(CompetitionPanel);
    }

    void SetPanel(string title, GoalRequirements req, string tag)
    {
        SetPanel(title, req.have, req.need, tag);
    }
    void SetPanel(string title, long have, long requirement, string tag)
    {
        ShowGoalPanel();

        Description.text = title;
        ProgressBar.SetValue(have, requirement);
        ProgressBar.SetDescription(tag);
    }
}

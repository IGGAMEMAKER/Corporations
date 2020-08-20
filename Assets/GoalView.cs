using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : View
{
    public Text Description;
    public ProgressBar ProgressBar;

    public override void ViewRender()
    {
        base.ViewRender();

        var clients = Marketing.GetClients(Flagship);
        var goal = Flagship.companyGoal.InvestorGoal;

        switch (goal)
        {
            case InvestorGoal.Prototype:
                var loyalty = Marketing.GetSegmentLoyalty(Q, Flagship, 0);

                SetPanel("Make test audience loyal", (long)loyalty, 1, "Loyalty");
                break;

            case InvestorGoal.FirstUsers:
                SetPanel("Accumulate 50K users", clients, 50000, "Users");
                break;

            case InvestorGoal.Release:
                SetPanel("Accumulate 1M users", clients, 1000000, "Users");
                break;

            case InvestorGoal.BecomeProfitable:
                var income = Economy.GetCompanyIncome(Q, Flagship);

                SetPanel("Increase your income", income, 500000, $"Income from product");
                break;
        }
    }

    void SetPanel(string title, long have, long requirement, string tag)
    {
        Description.text = title;
        ProgressBar.SetValue(have, requirement);
        ProgressBar.SetDescription(tag);
    }
}

using Assets.Core;
using UnityEngine.UI;

public class InvestmentGoalView : View
{
    public Text Goal;
    public ProgressBar ProgressBar;

    public Text NeedLabel;
    public Text NeedValue;

    public Text HaveLabel;
    public Text HaveValue;

    bool hasProgressBar => ProgressBar != null;

    void RenderProgress(GoalRequirements requirements, InvestorGoalType investorGoal)
    {
        switch (investorGoal)
        {
            case InvestorGoalType.Prototype:
                NeedLabel.text = "Improved Core Users";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Core Users Improvements";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoalType.Release:
                NeedLabel.text = "Release your app";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = ""; // ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoalType.FirstUsers:
                NeedLabel.text = "Required amount of users";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current amount of users";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoalType.BecomeMarketFit:
                NeedLabel.text = "Required product level";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current product level";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoalType.BecomeProfitable:
                NeedLabel.text = "Required Profit";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Our Profit";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoalType.GrowCompanyCost:
                NeedLabel.text = "Required Company Cost";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current Company Cost";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoalType.IPO:
                NeedLabel.text = "Organise IPO!";
                NeedValue.text = ""; // ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = "";
                break;
        }

        if (hasProgressBar)
            ProgressBar.SetValue(requirements.have, requirements.need);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goal = MyCompany.companyGoal.InvestorGoal;

        var goalInfo = Investments.GetFormattedInvestorGoal(goal);
        //var requirements = Investments.GetGoalRequirements(MyCompany, Q, goal);

        Goal.text = "InvestmentGoalView.cs"; // Visuals.Colorize(goalInfo, Investments.IsGoalCompleted(MyCompany, Q));

        //RenderProgress(requirements[0], goal);
    }
}

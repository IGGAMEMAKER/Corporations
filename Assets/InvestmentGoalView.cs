using Assets.Utils;
using UnityEngine.UI;

struct GoalViewInfo
{
    public string goal;
}

public class InvestmentGoalView : View
{
    public Text Goal;
    //public ProgressBar ProgressBar;

    public Text NeedLabel;
    public Text NeedValue;

    public Text HaveLabel;
    public Text HaveValue;

    CompanyGoalComponent goal
    {
        get
        {
            return MyCompany.companyGoal;
        }
    }

    GoalViewInfo GetGoalViewInfo()
    {
        return new GoalViewInfo
        {
            goal = InvestmentUtils.GetFormattedInvestorGoal(goal.InvestorGoal)
        };
    }

    void RenderProgress(GoalRequirements requirements, InvestorGoal investorGoal)
    {
        switch (investorGoal)
        {
            case InvestorGoal.Prototype:
                NeedLabel.text = "Improved Core Users";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Core Users Improvements";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoal.Release:
                NeedLabel.text = "Release your app";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = ""; // ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.FirstUsers:
                NeedLabel.text = "Required amount of users";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current amount of users";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoal.BecomeMarketFit:
                NeedLabel.text = "Required product level";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current product level";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoal.BecomeProfitable:
                NeedLabel.text = "Required Profit";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Our Profit";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoal.GrowCompanyCost:
                NeedLabel.text = "Required Company Cost";
                NeedValue.text = Format.Minify(requirements.need);

                HaveLabel.text = "Current Company Cost";
                HaveValue.text = Format.Minify(requirements.have);
                break;

            case InvestorGoal.IPO:
                NeedLabel.text = "Organise IPO!";
                NeedValue.text = ""; // ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = "";
                break;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goalinfo = GetGoalViewInfo();
        var requirements = InvestmentUtils.GetGoalRequirements(MyCompany, GameContext);

        Goal.text = Visuals.Colorize(goalinfo.goal, InvestmentUtils.IsGoalCompleted(MyCompany, GameContext));

        RenderProgress(requirements, goal.InvestorGoal);
    }
}

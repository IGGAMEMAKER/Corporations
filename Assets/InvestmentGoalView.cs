using Assets.Utils;
using UnityEngine.UI;

struct GoalViewInfo
{
    public int expires;
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

    GoalViewInfo GetGoalViewInfo()
    {
        var goal = MyProductEntity.companyGoal;

        return new GoalViewInfo
        {
            expires = goal.Expires - CurrentIntDate,
            goal = InvestmentUtils.GetFormattedInvestorGoal(goal.InvestorGoal)
        };
    }

    void RenderProgress(GoalRequirements requirements)
    {
        switch (MyProductEntity.companyGoal.InvestorGoal)
        {
            case InvestorGoal.Prototype:
                NeedLabel.text = "Improved Core Users";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "Core Users Improvements";
                HaveValue.text = Format.Shorten(requirements.have);
                break;

            case InvestorGoal.Release:
                NeedLabel.text = "Release your app";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = ""; // ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.FirstUsers:
                NeedLabel.text = "Required amount of users";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "Current amount of users";
                HaveValue.text = Format.Shorten(requirements.have);
                break;

            case InvestorGoal.BecomeMarketFit:
                NeedLabel.text = "Required product level";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "Current product level";
                HaveValue.text = Format.Shorten(requirements.have);
                break;

            case InvestorGoal.BecomeProfitable:
                NeedLabel.text = "Required Profit";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "Our Profit";
                HaveValue.text = Format.Shorten(requirements.have);
                break;

            case InvestorGoal.GrowCompanyCost:
                NeedLabel.text = "Required Company Cost";
                NeedValue.text = Format.Shorten(requirements.need);

                HaveLabel.text = "Current Company Cost";
                HaveValue.text = Format.Shorten(requirements.have);
                break;

            case InvestorGoal.IPO:
                NeedLabel.text = "Organise IPO!";
                NeedValue.text = ""; // ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = Format.Shorten(requirements.have);
                break;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goalinfo = GetGoalViewInfo();
        var requirements = InvestmentUtils.GetGoalRequirements(MyProductEntity, GameContext);

        Goal.text = Visuals.Colorize(goalinfo.goal, InvestmentUtils.IsGoalCompleted(MyProductEntity, GameContext));

        RenderProgress(requirements);
    }
}

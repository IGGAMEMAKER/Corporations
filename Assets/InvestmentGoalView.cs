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
                NeedLabel.text = "Improvements";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "Current product level";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.Release:
                NeedLabel.text = "Press release button";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = ""; // ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.FirstUsers:
                NeedLabel.text = "Required amount of users";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "Current amount of users";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.BecomeMarketFit:
                NeedLabel.text = "Required product level";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "Current product level";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.BecomeProfitable:
                NeedLabel.text = "Required Profit";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "Our Profit";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.GrowCompanyCost:
                NeedLabel.text = "Required Company Cost";
                NeedValue.text = ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "Current Company Cost";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
                break;

            case InvestorGoal.IPO:
                NeedLabel.text = "Make your company public!";
                NeedValue.text = "Organise IPO"; // ValueFormatter.Shorten(requirements.need);

                HaveLabel.text = "";
                HaveValue.text = ValueFormatter.Shorten(requirements.have);
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

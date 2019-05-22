using Assets.Utils;
using UnityEngine.UI;

struct GoalViewInfo
{
    public int expires;
    public string goal;
}

public class InvestmentGoalView : View
{
    public Text Expires;
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
                NeedValue.text = ""; // ValueFormatter.Shorten(requirements.need);

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

        Expires.text = goalinfo.expires + " days";

        Goal.text = VisualUtils.Colorize(goalinfo.goal, InvestmentUtils.IsGoalCompleted(MyProductEntity, GameContext));

        RenderProgress(requirements);
    }
}

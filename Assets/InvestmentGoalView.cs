using Assets.Utils;
using UnityEngine.UI;

struct GoalViewInfo
{
    public int expires;
    public string goal;
}

public class InvestmentGoalView : View
    , IAnyDateListener
{
    public Text Expires;
    public Text Goal;
    public ProgressBar ProgressBar;

    void Start()
    {
        Render();

        ListenDateChanges(this);
    }

    GoalViewInfo GetGoalViewInfo()
    {
        var goal = MyProductEntity.companyGoal;

        return new GoalViewInfo
        {
            expires = goal.Expires - CurrentIntDate,
            goal = InvestmentUtils.GetFormattedInvestorGoal(goal.InvestorGoal)
        };
    }

    void Render()
    {
        var goalinfo = GetGoalViewInfo();
        var requirements = InvestmentUtils.GetGoalViewRequirementsInfo(MyProductEntity, GameContext);

        Expires.text = goalinfo.expires + " days";

        Goal.text = VisualUtils.Colorize(goalinfo.goal, InvestmentUtils.IsGoalCompleted(MyProductEntity, GameContext));

        ProgressBar.SetValue(requirements.have, requirements.need);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

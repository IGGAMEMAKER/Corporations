using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public struct GoalViewInfo
{
    public int expires;
    public string goal;

    public long have;
    public long need;
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
        var me = MyProductEntity;
        var best = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.product.Niche);

        return new GoalViewInfo
        {
            expires = 58,
            have = me.product.ProductLevel,
            need = best.product.ProductLevel,
            goal = "Become market fit"
        };
    }

    void Render()
    {
        var goalinfo = GetGoalViewInfo();

        Expires.text = goalinfo.expires + " days";

        string col = goalinfo.have >= goalinfo.need ?
            VisualConstants.COLOR_POSITIVE
            :
            VisualConstants.COLOR_NEGATIVE;

        Goal.text = VisualFormattingUtils.Colorize(goalinfo.goal, col);

        ProgressBar.SetValue(goalinfo.have, goalinfo.need);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}

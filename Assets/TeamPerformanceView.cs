using Assets.Utils;

public class TeamPerformanceView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        int performance = TeamUtils.GetPerformance(GameContext, MyProductEntity);

        GetComponent<ColoredValueGradient>().UpdateValue(performance);
    }
}

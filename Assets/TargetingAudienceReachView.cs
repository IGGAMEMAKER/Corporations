using Assets.Utils;

public class TargetingAudienceReachView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<ColoredValue>().UpdateValue(MarketingUtils.GetTargetingEffeciency(GameContext, MyProductEntity));
    }
}

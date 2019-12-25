using Assets.Utils;

public class TargetingAudienceReachView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        // MarketingUtils.GetTargetingEffeciency(GameContext, MyProductEntity)
        GetComponent<ColoredValue>().UpdateValue(228228);
    }
}

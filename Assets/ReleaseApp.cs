using Assets.Utils;

public class ReleaseApp : ButtonController
{
    public override void Execute()
    {
        MarketingUtils.ReleaseApp(MyProductEntity);
    }
}

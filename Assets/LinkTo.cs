public class LinkTo : ButtonController
{
    public ScreenMode TargetMenu;

    public override void Execute()
    {
        Navigate(TargetMenu, null);
    }
}

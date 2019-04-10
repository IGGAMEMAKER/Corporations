public class LinkTo : ButtonController
{
    public ScreenMode TargetMenu;

    public override void Execute()
    {
        switch (TargetMenu)
        {
            case ScreenMode.CharacterScreen:
                Navigate(TargetMenu, null);
                break;
            case ScreenMode.IndustryScreen:
                Navigate(TargetMenu, IndustryType.Search);
                break;
            default:
                Navigate(TargetMenu, null);
                break;
        }
    }
}

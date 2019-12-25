public class LinkToNicheInfo : ButtonController
{
    NicheType Niche;

    public void SetNiche(NicheType niche)
    {
        Niche = niche;
    }

    public override void Execute()
    {
        Navigate(ScreenMode.NicheInfoScreen, Constants.MENU_SELECTED_NICHE, Niche);
    }
}

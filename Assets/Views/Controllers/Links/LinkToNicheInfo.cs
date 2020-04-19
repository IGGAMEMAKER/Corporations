public class LinkToNicheInfo : ButtonController
{
    NicheType Niche;

    public void SetNiche(NicheType niche)
    {
        Niche = niche;
    }

    public override void Execute()
    {
        Navigate(ScreenMode.NicheScreen, C.MENU_SELECTED_NICHE, Niche);
    }
}

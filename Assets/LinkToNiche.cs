public class LinkToNiche : ButtonController
{
    NicheType Niche;

    public void SetNiche(NicheType niche)
    {
        Niche = niche;
    }

    public override void Execute()
    {
        Navigate(ScreenMode.NicheScreen);

        SetSelectedNiche(Niche);
    }
}

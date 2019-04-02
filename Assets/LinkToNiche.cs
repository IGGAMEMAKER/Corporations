public class LinkToNiche : ButtonController
{
    Niche Niche;

    public void SetNiche(Niche niche)
    {
        Niche = niche;
    }

    public override void Execute()
    {
        Navigate(ScreenMode.MarketScreen);

        SetSelectedNiche(Niche);
    }
}

public class LinkToNiche : ButtonController
{
    NicheType Niche;

    public void SetNiche(NicheType niche)
    {
        Niche = niche;
    }

    public override void Execute()
    {
        NavigateToNiche(Niche);
    }
}

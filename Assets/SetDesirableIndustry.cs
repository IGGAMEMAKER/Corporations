using Assets.Utils;

public class SetDesirableIndustry : View
{
    void OnEnable()
    {
        var industry = NicheUtils.GetIndustry(ScreenUtils.GetSelectedNiche(GameContext), GameContext);

        GetComponent<LinkToResearchView>().SetIndustry(industry);
    }
}

using Assets.Utils;

public class SetDesirableIndustry : View
{
    void OnEnable()
    {
        var industry = Markets.GetIndustry(ScreenUtils.GetSelectedNiche(GameContext), GameContext);

        GetComponent<LinkToResearchView>().SetIndustry(industry);
    }
}

using Assets.Utils;

public class SetDesirableIndustry : View
{
    void OnEnable()
    {
        var industry = NicheUtils.GetIndustry(MenuUtils.GetNiche(GameContext), GameContext);

        GetComponent<LinkToResearchView>().SetIndustry(industry);
    }
}

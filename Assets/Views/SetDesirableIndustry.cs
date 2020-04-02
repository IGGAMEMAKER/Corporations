using Assets.Core;

public class SetDesirableIndustry : View
{
    void OnEnable()
    {
        var industry = Markets.GetIndustry(ScreenUtils.GetSelectedNiche(Q), Q);

        GetComponent<LinkToResearchView>().SetIndustry(industry);
    }
}

using Assets.Core;

public class SetDesirableIndustry : View
{
    void OnEnable()
    {
        var industry = Markets.GetIndustry(SelectedNiche, Q);

        GetComponent<LinkToResearchView>().SetIndustry(industry);
    }
}

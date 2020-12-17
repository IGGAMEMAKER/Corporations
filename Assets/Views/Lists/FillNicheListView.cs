using Assets.Core;

public class FillNicheListView : View
{
    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        var industry = ScreenUtils.GetSelectedIndustry(Q);
        var niches = Markets.GetObservableNichesInIndustry(industry, Q);

        GetComponent<NicheListView>().SetItems(niches);
    }
}

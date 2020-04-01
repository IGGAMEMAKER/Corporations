using Assets.Core;
using Entitas;
using System;

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

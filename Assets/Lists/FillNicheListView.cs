using Assets.Utils;
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
        var industry = ScreenUtils.GetSelectedIndustry(GameContext);
        var niches = NicheUtils.GetObservableNichesInIndustry(industry, GameContext);

        GetComponent<NicheListView>().SetItems(niches);
    }
}

using Assets.Utils;
using Entitas;
using System;

public class FillNicheListView : View
{
    void OnEnable()
    {
        Render();
    }

    // TODO PREDICATE
    //Predicate<GameEntity> FilterNichesByIndustry(IndustryType industry)
    //{
    //    return n => n.niche.IndustryType == industry; // && n.niche.NicheType != NicheType.None;
    //}

    void Render()
    {
        var industry = ScreenUtils.GetSelectedIndustry(GameContext);
        var niches = NicheUtils.GetObservableNichesInIndustry(industry, GameContext);

        GetComponent<NicheListView>().SetItems(niches);
    }
}

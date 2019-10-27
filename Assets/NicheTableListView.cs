using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NicheTableListView : ListView
{
    bool IncludeInnovativeMarkets = true;
    bool IncludeTrendingMarkets = true;
    bool IncludeMassMarkets = true;

    bool IncludeLessThan1MStartCapital = true;
    bool IncludeLessThan100MStartCapital = true;
    bool IncludeHugeStartCapital = true;

    bool HomeMarketsOnly = false;
    bool UnknownIndustriesOnly = false;
    bool BothMarkets = false;

    // --------

    public Toggle InnovativeMarkets;
    public Toggle TrendingMarkets;
    public Toggle MassMarkets;

    public Toggle CapitalSmall;
    public Toggle CapitalMid;
    public Toggle CapitalHigh;

    public Toggle AdjacentMarkets;
    public Toggle NonAdjacentMarkets;
    public Toggle AllMarkets;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<NicheTableView>().SetEntity(entity as GameEntity);
    }

    private void OnEnable()
    {
        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void UpdateFilters()
    {
        IncludeInnovativeMarkets = InnovativeMarkets.isOn;
        IncludeTrendingMarkets = TrendingMarkets.isOn;
        IncludeMassMarkets = MassMarkets.isOn;

        IncludeLessThan1MStartCapital = CapitalSmall.isOn;
        IncludeLessThan100MStartCapital = CapitalMid.isOn;
        IncludeHugeStartCapital = CapitalHigh.isOn;

        HomeMarketsOnly = AdjacentMarkets.isOn;
        UnknownIndustriesOnly = NonAdjacentMarkets.isOn;
        BothMarkets = AllMarkets.isOn;
    }

    bool IsSuitableByMarketState (GameEntity niche)
    {
        var state = NicheUtils.GetMarketState(niche);

        return (IncludeInnovativeMarkets && state == NicheLifecyclePhase.Innovation)
            || (IncludeTrendingMarkets && state == NicheLifecyclePhase.Trending)
            || (IncludeMassMarkets && state == NicheLifecyclePhase.MassUse);
    }

    bool IsSuitableByCapitalSize (GameEntity niche)
    {
        var capital = NicheUtils.GetStartCapital(niche);

        return (IncludeLessThan1MStartCapital && capital < 1000000)
            || (IncludeLessThan100MStartCapital && capital < 1000000 * 100)
            || (IncludeHugeStartCapital && capital >= 1000000 * 100);
    }

    bool IsConnectedToOurMainBusiness (GameEntity niche)
    {
        var isAdjacent = NicheUtils.IsAdjacentToCompanyInterest(niche, MyCompany);

        if (HomeMarketsOnly)
            return isAdjacent;

        if (UnknownIndustriesOnly)
            return !isAdjacent;

        return true;
    }

    public void Render()
    {
        UpdateFilters();

        var niches = NicheUtils.GetPlayableNiches(GameContext)
            .Where(IsSuitableByMarketState)
            .Where(IsSuitableByCapitalSize)
            .Where(IsConnectedToOurMainBusiness)
            .ToArray();

        GetComponent<NicheTableListView>().SetItems(niches);
    }
}

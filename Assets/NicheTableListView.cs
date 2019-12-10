using Assets.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NicheTableListView : ListView
{
    bool IncludeInnovativeMarkets = true;
    bool IncludeTrendingMarkets = true;
    bool IncludeMassMarkets = true;

    bool IncludeOnlyAffordableByStartCapital = true;

    bool HomeMarketsOnly = false;
    bool UnknownIndustriesOnly = false;
    bool BothMarkets = false;

    // --------

    public Toggle InnovativeMarkets;
    public Toggle TrendingMarkets;
    public Toggle MassMarkets;

    // enough money
    public Toggle AffordableByStartCapital;

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
        IncludeInnovativeMarkets = false; // InnovativeMarkets.isOn;
        IncludeTrendingMarkets = TrendingMarkets.isOn;
        IncludeMassMarkets = MassMarkets.isOn;

        IncludeOnlyAffordableByStartCapital = AffordableByStartCapital.isOn;

        HomeMarketsOnly = AdjacentMarkets.isOn;
        UnknownIndustriesOnly = NonAdjacentMarkets.isOn;
        BothMarkets = AllMarkets.isOn;
    }

    bool IsSuitableByMarketState(GameEntity niche)
    {
        var state = Markets.GetMarketState(niche);

        return (IncludeInnovativeMarkets && state == NicheState.Innovation)
            || (IncludeTrendingMarkets && state == NicheState.Trending)
            || (IncludeMassMarkets && (state == NicheState.MassGrowth || state == NicheState.MassUsage))
            ;
    }

    bool IsSuitableByCapitalSize (GameEntity niche)
    {
        var capital = Markets.GetStartCapital(niche);

        if (!IncludeOnlyAffordableByStartCapital)
            return true;

        return Companies.IsEnoughResources(MyCompany, capital);
    }

    bool IsConnectedToOurMainBusiness (GameEntity niche)
    {
        var isAdjacent = Markets.IsAdjacentToCompanyInterest(niche, MyCompany);

        if (HomeMarketsOnly)
            return isAdjacent;

        if (UnknownIndustriesOnly)
            return !isAdjacent;

        return true;
    }

    public void Render()
    {
        UpdateFilters();

        var niches = HasCompany ? Markets.GetPlayableNiches(GameContext)
            .Where(IsSuitableByMarketState)
            .Where(IsSuitableByCapitalSize)
            .Where(IsConnectedToOurMainBusiness)
            .ToArray()
            :
            null;

        GetComponent<NicheTableListView>().SetItems(niches);
    }
}

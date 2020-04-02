using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketResearchFilters : MonoBehaviour
{
    public NicheTableListView NicheTableListView;

    public void ToggleInnovativeMarkets() => UpdateMarketList();

    public void ToggleTrendingMarkets() => UpdateMarketList();

    public void ToggleMassMarkets()
    {
        UpdateMarketList();
    }



    public void ToggleStartCapitalLessThan1Million()
    {
        UpdateMarketList();
    }

    public void ToggleStartCapitalLessThan100Million()
    {
        UpdateMarketList();
    }

    public void ToggleStartCapitalBiggest()
    {
        UpdateMarketList();
    }

    public void SetHomeMarketsOnly()
    {
        UpdateMarketList();
    }

    public void SetAllIndustries()
    {
        UpdateMarketList();
    }

    void UpdateMarketList()
    {
        NicheTableListView.Render();
    }
}

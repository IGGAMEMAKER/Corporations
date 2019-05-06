using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMarketingFinancingController : ButtonController
{
    public MarketingFinancing marketingFinancing;

    public override void Execute()
    {
        MarketingUtils.SetFinancing(GameContext, MyProductEntity.company.Id, marketingFinancing);
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.finance.marketingFinancing == marketingFinancing);
    }
}

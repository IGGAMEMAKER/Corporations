using UnityEngine.UI;
using Entitas;
using System;
using Assets.Utils;

public class MostPopularApplicationView : View
{
    Text MarketRequirements;
    Hint Hint;

    void Start()
    {
        MarketRequirements = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void Render()
    {
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProduct.Niche);

        AnimateIfValueChanged(MarketRequirements, bestApp.product.Name);

        Hint.SetHint($"{MarketingUtils.GetClients(bestApp)} clients");
    }
}

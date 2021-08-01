using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelsListView2 : ListView
{
    public Text NoAvailableFeaturesText;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MarketingChannelView>().SetEntity(entity as ChannelInfo, 0, 100);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Take(3);

        var count = channels.Count();

        if (count == 0)
        {
            NoAvailableFeaturesText.text = Visuals.Negative($"You need more money to add more channels!");
        }

        Draw(NoAvailableFeaturesText, count == 0);

        SetItems(channels);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

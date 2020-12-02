using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelsListView2 : ListView
{
    public Text NoAvailableFeaturesText;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MarketingChannelView>().SetEntity(entity as GameEntity, 0, 100);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var channels = Markets.GetAffordableMarketingChannels(Flagship, Q).Take(3);

        var count = channels.Count();

        if (count == 0)
        {
            if (Teams.HasFreeSlotForTeamTask(Flagship, Teams.GetMarketingTaskMockup()))
            {
                NoAvailableFeaturesText.text = Visuals.Negative($"You need more money to add more channels!");
            }
            else
            {
                NoAvailableFeaturesText.text = $"Hire and upgrade more teams, to add more marketing channels";
            }
        }

        Draw(NoAvailableFeaturesText, count == 0);

        SetItems(channels);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}

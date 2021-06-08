using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketingCampaignView : View
{
    public Text Name;
    public Text Cost;
    public Text Gain;

    public GameObject Fade;
    public ProgressBar ProgressBar;

    public void SetEntity(MarketingChannelComponent marketingChannelComponent)
    {
        var info = marketingChannelComponent.ChannelInfo;

        Name.text = "Channel " + info.ID;
        Cost.text = Format.Money(info.relativeCost);
        Gain.text = Format.MinifyToInteger(info.Audience);

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var f = CurrentIntDate % 30;

        ProgressBar.SetValue(f, 15);
        ProgressBar.SetCustomText("asdsd");

        Draw(Fade, f <= 15);
        Draw(ProgressBar, f <= 15);
    }
}

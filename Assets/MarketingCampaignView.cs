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

    public MarketingChannelController marketingChannelController;

    ChannelInfo ChannelInfo;

    public void SetEntity(ChannelInfo info)
    {
        ChannelInfo = info;

        Name.text = "Channel " + info.ID;
        
        Cost.text = Format.Money(info.costPerAd);
        Gain.text = Format.MinifyToInteger(info.Batch);

        marketingChannelController.SetEntity(info);

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var f = CurrentIntDate % 30;

        ProgressBar.SetValue(f, 15);
        ProgressBar.SetCustomText("asdsd");

        bool isUpgrading = Marketing.IsActiveInChannel(Flagship, ChannelInfo.ID);

        Draw(Fade, isUpgrading);
        Draw(ProgressBar, isUpgrading);
    }
}

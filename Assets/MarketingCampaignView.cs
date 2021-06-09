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
    Transform animationTransform;

    public void SetEntity(ChannelInfo info, Transform animationTransform)
    {
        ChannelInfo = info;
        this.animationTransform = animationTransform;

        Name.text = "Channel " + info.ID;



        marketingChannelController.SetEntity(info);

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        var cost = Marketing.GetChannelCost(product, ChannelInfo.ID);
        cost = (long)ChannelInfo.costPerAd;

        var clients = Marketing.GetChannelClientGain(product, ChannelInfo, 0);


        Cost.text = Format.Money(cost, true);
        Gain.text = Format.Minify(clients);

        var period = 30;
        var f = CurrentIntDate % period;

        ProgressBar.SetValue(f, period);
        ProgressBar.SetCustomText("Execution");


        bool isUpgrading = Marketing.IsActiveInChannel(product, ChannelInfo.ID);

        Draw(Fade, isUpgrading);
        Draw(ProgressBar, isUpgrading);

        if (ScheduleUtils.IsMonthEnd(CurrentIntDate) && isUpgrading)
        {
            Marketing.DisableChannelActivity(product, Markets.GetMarketingChannel(Q, ChannelInfo.ID));
            Marketing.AddClients(product, clients, 0);

            // Spawn animation
            FindObjectOfType<AnimationSpawner>().Spawn(Visuals.Positive($"+{Format.Minify(clients)} users"), transform);
        }
    }
}

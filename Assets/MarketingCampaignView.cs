using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var channelId = ChannelInfo.ID;

        var cost = Marketing.GetChannelCost(product, channelId);
        cost = (long)ChannelInfo.costPerAd;

        var clients = Marketing.GetChannelClientGain(product, ChannelInfo, 0);


        Cost.text = Format.Money(cost, true);
        Gain.text = "+" + Format.Minify(clients) + " users";

        // 2K => 3,3
        // 10M => 6
        var duration = Mathf.Log(clients, 5);
        var period = C.PERIOD * duration;
        var f = CurrentIntDate % period;

        ProgressBar.SetValue(f, period);
        ProgressBar.SetCustomText(Mathf.Ceil(f * 100 / period) + "%");


        bool isUpgrading = Marketing.IsActiveInChannel(product, channelId);
        bool needsToEnd = f < 2; // ScheduleUtils.IsMonthEnd(CurrentIntDate);

        Draw(Fade, isUpgrading);
        Draw(ProgressBar, isUpgrading);

        if (needsToEnd && isUpgrading)
        {
            var task = product.team.Teams[0].Tasks.First(t => t.IsMarketingTask && (t as TeamTaskChannelActivity).ChannelId == channelId);

            Teams.RemoveTeamTask(Flagship, Q, task);
            //Marketing.DisableChannelActivity(product, Markets.GetMarketingChannel(Q, channelId));
            Marketing.AddClients(product, clients, 0);

            // Spawn animation
            Animate(Visuals.Positive($"+{Format.Minify(clients)} users"));
        }
    }
}

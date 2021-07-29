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

    public Text TextualProgress;

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

        var clients = Marketing.GetChannelClientGain(product, ChannelInfo);


        Cost.text = "for " + Format.Money(cost, true);
        Gain.text = Visuals.Positive("+" + Format.MinifyToInteger(clients));

        // 2K => 3,3
        // 10M => 6
        bool isUpgrading = Marketing.IsActiveInChannel(product, channelId);


        var duration = Marketing.GetCampaignDuration(product, ChannelInfo); // Mathf.Log(clients, 5) / 2;
        var period = C.PERIOD * duration;
        var f = CurrentIntDate % period;

        var percent = Mathf.Ceil(f * 100 / period);


        if (isUpgrading)
        {
            var task = product.team.Teams[0].Tasks
                .First(t => t.IsMarketingTask && (t as TeamTaskChannelActivity).ChannelId == channelId);

            //Debug.Log("curr " + Format.FormatDate(CurrentIntDate) + " start " + Format.FormatDate(task.StartDate) + " end " + Format.FormatDate(task.EndDate));

            RenderChannelProgress((CurrentIntDate - task.StartDate) * 100 / (task.EndDate - task.StartDate));
        }
        else
        {
            RenderChannelProgress(0);
        }

        /*ProgressBar.SetValue(f, period);
        ProgressBar.SetCustomText(percent + "%");

        if (TextualProgress)
            TextualProgress.text = percent + "%";*/



        Draw(Fade, isUpgrading);
        Draw(ProgressBar, isUpgrading);

        return;
        bool needsToEnd = f < 2; // ScheduleUtils.IsMonthEnd(CurrentIntDate);

        /*if (needsToEnd && isUpgrading)
        {
            var task = product.team.Teams[0].Tasks.First(t => t.IsMarketingTask && (t as TeamTaskChannelActivity).ChannelId == channelId);

            //task.StartDate =

            Teams.RemoveTeamTask(Flagship, Q, task);
            //Marketing.AddClients(product, clients);

            // Spawn animation
            //Animate(Visuals.Positive($"+{Format.Minify(clients)} users"));
        }*/
    }

    void RenderChannelProgress(int percent)
    {
        //ProgressBar.SetValue(f, period);
        ProgressBar.SetValue(percent, 100);
        ProgressBar.SetCustomText(percent + "%");

        if (TextualProgress)
            TextualProgress.text = percent + "%";
    }
}

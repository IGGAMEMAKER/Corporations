﻿using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTaskView : View
{
    public int TeamId;
    public int SlotId;

    public Image Icon;

    public Sprite FeatureSprite;
    public Sprite MonetisationSprite;
    public Sprite ChannelSprite;

    public Sprite SupportSprite;
    public Sprite ServerSprite;
    public Sprite MarketingSupportSprite;

    public Text RepresentativeNumber;
    public Hint TaskHint;

    public Image ProgressImage;
    public Image ProgressIcon;

    public void SetEntity(int TeamId, int SlotId)
    {
        this.TeamId = TeamId;
        this.SlotId = SlotId;

        ViewRender();
    }

    public TeamTask Task => Flagship.team.Teams[TeamId].Tasks[SlotId];

    public bool IsFeatureUpgradeTask => Task is TeamTaskFeatureUpgrade;
    public bool IsChannelTask => Task is TeamTaskChannelActivity;
    public bool IsSupportTask => Task is TeamTaskSupportFeature;
    public bool IsServerTask => IsSupportTask && (Task as TeamTaskSupportFeature).SupportFeature.SupportBonus is SupportBonusHighload;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

            var tasks = product.team.Teams[TeamId].Tasks;

            if (SlotId >= tasks.Count)
                return;

            var task = tasks[SlotId];

        if (IsFeatureUpgradeTask)
        {
            // set image
            Icon.sprite = FeatureSprite;

            if ((Task as TeamTaskFeatureUpgrade).NewProductFeature.FeatureBonus.isMonetisationFeature)
            {
                Icon.sprite = MonetisationSprite;
            }

            var featureName = (task as TeamTaskFeatureUpgrade).NewProductFeature.Name;
            var rating = Products.GetFeatureRating(product, featureName);

            RepresentativeNumber.text = rating.ToString("0.0");
            RepresentativeNumber.color = Visuals.GetGradientColor(0, 10, rating);

            TaskHint.SetHint("Upgrading feature " + featureName + 
                $"\n\nFeature quality:" +
                $"\n<size=50><b>{rating.ToString("0.0")} / 10</b></size>"
                );

            // feature upgrade progress
            var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";
            bool hasCooldown = Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown cooldown);


            var progress = CurrentIntDate - cooldown.StartDate;
            var percent = (float)progress / (cooldown.EndDate - cooldown.StartDate);

            ProgressIcon.fillAmount = percent;
            ProgressImage.sprite = Icon.sprite;

            Show(ProgressImage);
        }


        if (IsChannelTask)
        {
            Icon.sprite = ChannelSprite;

            var channel = Markets.GetMarketingChannel(Q, (task as TeamTaskChannelActivity).ChannelId);
            var gain = Marketing.GetChannelClientGain(product, Q, channel);

            RepresentativeNumber.text = "+" + Format.Minify(gain); // .ToString("0.0")
            RepresentativeNumber.color = Visuals.GetColorPositiveOrNegative(true);

            TaskHint.SetHint($"Getting {Format.Minify(gain)} users from channel Forum {channel.marketingChannel.ChannelInfo.ID}");

            Hide(ProgressImage);
        }


        if (IsSupportTask)
        {
            var supportFeature = task as TeamTaskSupportFeature;

            var bonus = supportFeature.SupportFeature.SupportBonus;

            var value = supportFeature.SupportFeature.SupportBonus.Max;

            if (bonus is SupportBonusHighload)
            {
                bool needsMoreServers = Products.IsNeedsMoreServers(product);

                var hint = "";

                if (needsMoreServers)
                    hint += Visuals.Negative("\n\nNOT ENOUGH SERVERS!\nPeople are leaving your product!");

                TaskHint.SetHint($"Servers for {Format.Minify(value)} users. " + hint);

                RepresentativeNumber.color = Visuals.GetColorPositiveOrNegative(!needsMoreServers);
                Icon.sprite = ServerSprite;
            }
            else if (bonus is SupportBonusMarketingSupport)
            {
                bool needsMoreSupport = Products.IsNeedsMoreMarketingSupport(product);

                var hint = "";
                if (needsMoreSupport)
                    hint += "\nNOT ENOUGH CLIENT SUPPORT!";

                TaskHint.SetHint($"Client support for {Format.Minify(value)} clients" + hint);

                RepresentativeNumber.color = Visuals.GetColorPositiveOrNegative(!needsMoreSupport);
                Icon.sprite = MarketingSupportSprite;
            }
            else
            {
                TaskHint.SetHint("WTF? ");
                RepresentativeNumber.color = Visuals.GetColorFromString(Colors.COLOR_POSITIVE);
                Icon.sprite = SupportSprite;
            }

            RepresentativeNumber.text = Format.Minify(value);

            Hide(ProgressImage);
        }
    }
}

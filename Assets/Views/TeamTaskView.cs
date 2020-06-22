using Assets.Core;
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

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var tasks = company.team.Teams[TeamId].Tasks;

        if (SlotId >= tasks.Count)
            return;

        var task = tasks[SlotId];

        var product = Flagship;

        if (IsFeatureUpgradeTask)
        {
            // set image
            Icon.sprite = FeatureSprite;

            var featureName = (task as TeamTaskFeatureUpgrade).NewProductFeature.Name;
            var rating = Products.GetFeatureRating(company, featureName);

            RepresentativeNumber.text = rating.ToString("0.0");
            RepresentativeNumber.color = Visuals.GetGradientColor(0, 10, rating);

            TaskHint.SetHint("Upgrading feature " + featureName + $"\n\nFeature quality: <b>{rating.ToString("0.0")} / 10</b>");

            // feature upgrade progress
            var cooldownName = $"company-{product.company.Id}-upgradeFeature-{featureName}";
            bool hasCooldown = Cooldowns.HasCooldown(Q, cooldownName, out SimpleCooldown cooldown);

            Show(ProgressImage);
            var progress = CurrentIntDate - cooldown.StartDate;
            var percent = (float)progress / (cooldown.EndDate - cooldown.StartDate);

            ProgressIcon.fillAmount = percent;
            ProgressImage.sprite = Icon.sprite;
        }


        if (IsChannelTask)
        {
            Icon.sprite = ChannelSprite;

            var channel = Markets.GetMarketingChannel(Q, (task as TeamTaskChannelActivity).ChannelId);
            var gain = Marketing.GetChannelClientGain(company, Q, channel);

            RepresentativeNumber.text = "+" + Format.Minify(gain); // .ToString("0.0")
            RepresentativeNumber.color = Visuals.GetColorPositiveOrNegative(true);

            TaskHint.SetHint($"Getting {Format.Minify(gain)} users from channel Forum {channel.marketingChannel.ChannelInfo.ID}");

            Hide(ProgressImage);
        }


        if (IsSupportTask)
        {
            var supportFeature = task as TeamTaskSupportFeature;

            var bonus = supportFeature.SupportFeature.SupportBonus;

            if (bonus is SupportBonusHighload)
                Icon.sprite = ServerSprite;
            else if (bonus is SupportBonusMarketingSupport)
                Icon.sprite = MarketingSupportSprite;
            else
                Icon.sprite = SupportSprite;

            RepresentativeNumber.text = Format.Minify(supportFeature.SupportFeature.SupportBonus.Max);
            RepresentativeNumber.color = Visuals.GetColorFromString(Colors.COLOR_POSITIVE);

            Hide(ProgressImage);
        }
    }
}

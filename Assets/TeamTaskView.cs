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

    public Text RepresentativeNumber;

    public void SetEntity(int TeamId, int SlotId)
    {
        this.TeamId = TeamId;
        this.SlotId = SlotId;

        ViewRender();
    }

    public TeamTask Task => Flagship.team.Teams[TeamId].Tasks[SlotId];

    public bool IsFeatureUpgradeTask => Task is TeamTaskFeatureUpgrade;
    public bool IsChannelTask => Task is TeamTaskChannelActivity;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var tasks = company.team.Teams[TeamId].Tasks;

        if (SlotId >= tasks.Count)
            return;

        var task = tasks[SlotId];

        bool isFeature = IsFeatureUpgradeTask; // task is TeamTaskFeatureUpgrade;
        bool isChannel = IsChannelTask; // task is TeamTaskChannelActivity;

        // set image
        Icon.sprite = isFeature ? FeatureSprite : ChannelSprite;

        if (isFeature)
        {
            var featureName = (task as TeamTaskFeatureUpgrade).NewProductFeature.Name;
            var rating = Products.GetFeatureRating(company, featureName);

            RepresentativeNumber.text = rating.ToString("0.0");
            RepresentativeNumber.color = Visuals.GetGradientColor(0, 10, rating);

            GetComponent<Hint>().SetHint("Upgrading feature " + featureName);
        }

        if (isChannel)
        {
            var channel = Markets.GetMarketingChannel(Q, (task as TeamTaskChannelActivity).ChannelId);
            var gain = Marketing.GetChannelClientGain(company, Q, channel);

            RepresentativeNumber.text = "+" + Format.Minify(gain); // .ToString("0.0")
            RepresentativeNumber.color = Visuals.GetColorPositiveOrNegative(true);

            GetComponent<Hint>().SetHint($"Getting {Format.Minify(gain)} users from channel Forum {channel.marketingChannel.ChannelInfo.ID}");
        }
    }
}

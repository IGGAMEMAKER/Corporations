using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelView : View
{
    public ChannelInfo channel;

    public Text Title;
    public Text Users;
    public Text Cost;

    public Text CostPerUser;

    public Image ChosenImage;

    public Image BackgroundImage; // 060F30

    public RawImage SegmentTypeImage;

    public Image ChosenCheckMark;

    float maxCost = 100;
    float minCost = 0;

    public void SetEntity(ChannelInfo channel, float minROI, float maxROI)
    {
        this.channel = channel;
        this.maxCost = maxROI;
        this.minCost = minROI;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        // basic info
        Title.text = $"Channel {channel.ID}";

        RenderAudienceGain(company);

        RenderSegmentImage(company);

        RenderCost(company);

        RenderActivitySigns(company, channel);
    }

    void RenderCost(GameEntity company)
    {
        var channelId = channel.ID;
        
        var adCost = Marketing.GetChannelCost(company, channelId);
        var clientCost = Marketing.GetChannelCostPerUser(company, channelId);
        var repaymentColor = Visuals.GetGradientColor(minCost, maxCost, clientCost, true);

        var canMaintain = Economy.IsCanMaintainForAWhile(MyCompany, Q, adCost, 1);

        var isFreeChannel = adCost == 0;

        Cost.text = isFreeChannel ? "FREE" : $"{Format.Money(adCost, true)} weekly"; //  (${clientCost.ToString("0.00")} each)
        Cost.color = Visuals.GetColorPositiveOrNegative(canMaintain);

        CostPerUser.text = $"{clientCost:0.0}$";
        CostPerUser.color = repaymentColor;
    }

    void RenderAudienceGain(GameEntity company)
    {
        var channelId = channel.ID;

        // audience gain
        var gain = Marketing.GetChannelClientGain(company, channelId);

        Users.text = "+" + Format.Minify(gain); // + " users";
    }

    void RenderSegmentImage(GameEntity company)
    {
        // audience icon
        if (Marketing.IsFocusingMoreThanOneAudience(company))
        {
            SegmentTypeImage.texture = Resources.Load<Texture2D>($"paidClients");
        }
        else
        {
            var segmentID = Marketing.GetBaseAudienceId(company);
            //var segmentID = Marketing.GetCoreAudienceId(company);
            var audiences = Marketing.GetAudienceInfos();

            SegmentTypeImage.texture = Resources.Load<Texture2D>($"Audiences/{audiences[segmentID].Icon}");
        }
    }

    void RenderActivitySigns(GameEntity company, ChannelInfo channel)
    {
        bool isActiveChannel = Marketing.IsActiveInChannel(company, channel.ID);

        Draw(ChosenImage, isActiveChannel);
        Draw(ChosenCheckMark, isActiveChannel);
    }
}

using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketingChannelView : View, IPointerEnterHandler, IPointerExitHandler
{
    public GameEntity channel;

    public Text Title;
    public Text Users;
    public Text Income;

    public CanvasGroup CanvasGroup;
    public Image ChosenImage;

    public RawImage SegmentTypeImage;

    public Image ChosenCheckMark;

    public Image ExplorationImage;
    public Image DomineeringIcon;

    public Text MarketingComplexity;

    float maxROI = 100;
    float minROI = 0;

    public override void ViewRender()
    {
        base.ViewRender();

        // some error
        if (channel == null)
            return;

        var marketingChannel = channel.marketingChannel;

        var channel1 = marketingChannel.ChannelInfo;

        var company = Flagship;

        var audiences = Marketing.GetAudienceInfos();
        var segmentID = company.productTargetAudience.SegmentId;

        // basic info
        var name = $"Channel {channel1.ID}";
        Title.text = name;

        var gainedAudience = Marketing.GetChannelClientGain(company, Q, channel);
        Users.text = "+" + Format.Minify(gainedAudience) + " users";

        var ROI = Marketing.GetChannelROI(company, Q, channel, segmentID);
        ROI = Marketing.GetChannelCostPerUser(company, Q, channel);

        var repaysSelf = Marketing.GetChannelRepaymentSpeed(company, Q, channel, segmentID);

        SegmentTypeImage.texture = Resources.Load($"Audiences/{audiences[segmentID].Icon}") as Texture2D;

        var adCost = Marketing.GetMarketingActivityCost(company, Q, channel);
        //var repaymentColor = Visuals.GetGradientColor(minROI, maxROI, ROI, true);
        var repaymentColor = Visuals.GetGradientColor(minROI, maxROI, ROI, true);
        //repaymentColor = Visuals.GetColorPositiveOrNegative(Economy.IsCanMaintain(company, Q, adCost));

        var clientCost = ROI; // adCost / gainedAudience;

        Income.text = $"for {Format.MinifyMoney(adCost)} (${clientCost.ToString("0.00")} each)";
        Income.color = repaymentColor;

        MarketingComplexity.text = $"{clientCost.ToString("0.0")}$";
        MarketingComplexity.color = repaymentColor;

        //MarketingComplexity.text = channel1.costInWorkers.ToString();

        Hide(MarketingComplexity);
        Hide(Income);

        bool isActiveChannel = Marketing.IsCompanyActiveInChannel(company, channel);
        CanvasGroup.alpha = 1;

        Draw(ChosenImage, isActiveChannel);
        Draw(ChosenCheckMark, isActiveChannel);

        var dayOfPeriod = CurrentIntDate % C.PERIOD;
        RenderProgress(isActiveChannel ? dayOfPeriod : 0, C.PERIOD);
    }

    void RenderProgress(float progress, float duration)
    {
        ExplorationImage.fillAmount = 1f - (duration - progress) / duration; // Random.Range(0, 1f);
    }

    public void SetEntity(GameEntity channel, float minROI, float maxROI)
    {
        this.channel = channel;
        this.maxROI = maxROI;
        this.minROI = minROI;

        ViewRender();
    }

    void ToggleTexts(bool showFace)
    {
        Draw(MarketingComplexity, false);
        Draw(Income, !showFace);

        //Draw(SegmentTypeImage, !showFace);
        //Draw(Users, !showFace);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ToggleTexts(false);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ToggleTexts(true);
    }
}

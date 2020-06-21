using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelView : View
{
    public GameEntity channel;

    public Text Title;
    public Text Users;
    public Text Income;

    public CanvasGroup CanvasGroup;
    public Image ChosenImage;

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

        // basic info
        var name = $"Forum {channel1.ID}";
        Title.text = name;

        var gainedAudience = Marketing.GetChannelClientGain(company, Q, channel);
        Users.text = "+" + Format.Minify(gainedAudience) + " users";

        var ROI = Marketing.GetChannelROI(company, Q, channel);

        var repaysSelf = Marketing.GetChannelRepaymentSpeed(company, Q, channel);



        var adCost = Marketing.GetMarketingActivityCost(company, Q, channel);
        var repaymentColor = Visuals.GetGradientColor(minROI, maxROI, ROI, true);

        Income.text = $"{Format.MinifyMoney(adCost)} / week";
        Income.color = repaymentColor;

        MarketingComplexity.text = $"{repaysSelf.ToString("0.0")}m";
        MarketingComplexity.color = repaymentColor;
        //MarketingComplexity.text = channel1.costInWorkers.ToString();

        bool isActiveChannel = Marketing.IsCompanyActiveInChannel(company, channel);
        CanvasGroup.alpha = 1; // isActiveChannel ? 1 : 0.92f;
        Draw(ChosenImage, isActiveChannel);
        Draw(ChosenCheckMark, isActiveChannel);

        var dayOfPeriod = CurrentIntDate % C.PERIOD;
        RenderProgress(isActiveChannel ? dayOfPeriod : 0, C.PERIOD);
    }

    void RenderProgress(float progress, float duration)
    {
        ExplorationImage.fillAmount = 1f - (duration - progress) / duration; // Random.Range(0, 1f);
    }

    public void SetEntity(GameEntity channel, float maxROI, float minROI)
    {
        this.channel = channel;
        this.maxROI = maxROI;
        this.minROI = minROI;

        ViewRender();
    }
}

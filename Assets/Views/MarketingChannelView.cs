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

    bool isExplorationMockup = false;

    float maxROI = 100;
    float minROI = 0;

    public override void ViewRender()
    {
        base.ViewRender();

        // some error
        if (channel == null && !isExplorationMockup)
            return;

        if (isExplorationMockup)
        {
            Users.text = "EXPLORE";
            Income.text = "";
            Title.text = "";

            Draw(ChosenImage, false);
            Draw(DomineeringIcon, false);
            Draw(ChosenCheckMark, false);

            return;
        }

        var marketingChannel = channel.marketingChannel;

        var channel1 = marketingChannel.ChannelInfo;

        var company = Flagship;

        // basic info
        var name = $"Forum {channel1.ID}";
        Title.text = name;
        var gainedAudience = Marketing.GetChannelClientGain(company, Q, channel);
        Users.text = "+" + Format.Minify(gainedAudience) + " users";
        //Users.text = "+" + Format.Minify(channel1.Batch) + " users";
        //Users.text = Format.Minify(channel1.Audience) + " users";

        var ROI = Marketing.GetChannelROI(company, Q, channel);

        var adCost = Marketing.GetMarketingActivityCost(company, Q, channel);
        //Income.text = $"ROI: {ROI.ToString("0.00")}%"; // ({lifetimeFormatted})
        Income.text = $"Cost: {Format.MinifyMoney(adCost)}"; // ({lifetimeFormatted})
        Income.color = Visuals.GetGradientColor(minROI, maxROI, ROI, true);
        //Income.color = Visuals.GetColorPositiveOrNegative(Economy.IsCanMaintain(company, Q, adCost));

        MarketingComplexity.text = channel1.costInWorkers.ToString();

        bool isActiveChannel = Marketing.IsCompanyActiveInChannel(company, channel);
        CanvasGroup.alpha = 1; // isActiveChannel ? 1 : 0.92f;
        Draw(ChosenImage, isActiveChannel);
        Draw(ChosenCheckMark, isActiveChannel);

        bool isExploredMarket = Marketing.IsChannelExplored(channel, company);

        //Debug.Log("Is Explored Market " + name + ": " + isExploredMarket);

        if (isExploredMarket)
        {
            var dayOfPeriod = CurrentIntDate % C.PERIOD;
            RenderProgress(isActiveChannel ? dayOfPeriod : 0, C.PERIOD);
        }
        else
        {
            Income.text = "???";
            Income.color = Visuals.GetColorFromString(Colors.COLOR_WHITE);

            Users.text = "+??? users";


            var exp = company.channelExploration;
            var duration = 10f;
            var progress = exp.InProgress.ContainsKey(channel1.ID) ? exp.InProgress[channel1.ID] : duration;

            RenderProgress(progress, duration);
        }
    }

    void RenderProgress(float progress, float duration)
    {
        ExplorationImage.fillAmount = 1f - (duration - progress) / duration; // Random.Range(0, 1f);
    }

    public void SetEntity(GameEntity channel, float maxROI, float minROI, bool isExplorationMockup)
    {
        this.channel = channel;
        this.maxROI = maxROI;
        this.minROI = minROI;

        this.isExplorationMockup = isExplorationMockup;

        ViewRender();
    }
}

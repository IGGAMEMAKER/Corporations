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

    bool isExplorationMockup = false;

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
        Users.text = "+" + Format.Minify(channel1.Batch) + " users";

        //Debug.Log("Rendering Market " + name);

        // income
        var lifetime = Marketing.GetLifeTime(Q, company.company.Id);
        var lifetimeFormatted = lifetime.ToString("0.00");

        var baseIncome = Economy.GetBaseSegmentIncome(Q, company, 0);
        var income = baseIncome * lifetime - channel1.costPerUser;
        var formattedIncome = income.ToString("0.00");

        Income.text = $"+${formattedIncome} / user"; // ({lifetimeFormatted})
        Income.color = Visuals.GetGradientColor(0, 5, income);


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

    public void SetEntity(GameEntity channel, bool isExplorationMockup)
    {
        this.channel = channel;

        this.isExplorationMockup = isExplorationMockup;

        ViewRender();
    }
}

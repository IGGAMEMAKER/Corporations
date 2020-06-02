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

    public Image ExplorationImage;

    public override void ViewRender()
    {
        base.ViewRender();

        if (channel == null)
            return;

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

        Income.text = $"+${formattedIncome} / user ({lifetimeFormatted})";
        Income.color = Visuals.GetGradientColor(0, 5, income);


        bool isChosen = Marketing.IsCompanyActiveInChannel(company, channel);
        CanvasGroup.alpha = isChosen ? 1 : 0.92f;
        Draw(ChosenImage, isChosen);

        bool isExploredMarket = Marketing.IsChannelExplored(channel, company);

        //Debug.Log("Is Explored Market " + name + ": " + isExploredMarket);

        if (isExploredMarket)
        {
            ExplorationImage.fillAmount = 0;
        }
        else
        {
            Income.text = "???";
            Income.color = Visuals.GetColorFromString(Colors.COLOR_WHITE);

            Users.text = "+??? users";


            var exp = company.channelExploration;
            var duration = 10f;
            var progress = exp.InProgress.ContainsKey(channel1.ID) ? exp.InProgress[channel1.ID] : duration;

            ExplorationImage.fillAmount = (duration - progress) / duration; // Random.Range(0, 1f);
        }
    }

    public void SetEntity(GameEntity channel)
    {
        this.channel = channel;

        ViewRender();
    }
}

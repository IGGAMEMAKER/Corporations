using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelView : View
{
    GameEntity channel;

    public Text Title;
    public Text Users;
    public Text Income;

    public override void ViewRender()
    {
        base.ViewRender();

        var marketingChannel = channel.marketingChannel;

        var channel1 = marketingChannel.ChannelInfo;
        channel1.Audience = marketingChannel.Clients;
        channel1.Batch = marketingChannel.Clients / 50;
        channel1.Companies = channel.companyMarketingActivities.Companies;

        Title.text = $"Forum {channel1.ID}";
        Users.text = Format.Minify(channel1.Audience) + " users";

        var income = 5 - channel1.costPerUser;
        var formattedIncome = income.ToString("0.00");

        Income.text = $"+${formattedIncome} / user";
        Income.color = Visuals.GetGradientColor(0, 5, income);
    }

    public void SetEntity(GameEntity channel)
    {
        this.channel = channel;

        ViewRender();
    }
}

using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class MarketingChannelView : View
{
    ChannelInfo channel;

    public Text Title;
    public Text Users;
    public Text Income;

    public override void ViewRender()
    {
        base.ViewRender();

        Title.text = $"Forum {channel.ID}";
        Users.text = Format.Minify(channel.Audience) + " users";

        var income = 5 - channel.costPerUser;
        var formattedIncome = income.ToString("0.00");

        Income.text = $"+${formattedIncome} / user";
        Income.color = Visuals.GetGradientColor(0, 5, income);
    }

    public void SetEntity(ChannelInfo channel)
    {
        this.channel = channel;

        ViewRender();
    }
}

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

        var marketingChannel = channel.marketingChannel;

        var channel1 = marketingChannel.ChannelInfo;

        Title.text = $"Forum {channel1.ID}";
        Users.text = "+" + Format.Minify(channel1.Batch) + " users";

        var lifetime = Marketing.GetLifeTime(Q, Flagship.company.Id);
        var lifetimeFormatted = lifetime.ToString("0.00");

        var baseIncome = Economy.GetBaseSegmentIncome(Q, Flagship, 0);
        var income = baseIncome * lifetime - channel1.costPerUser;
        var formattedIncome = income.ToString("0.00");

        Income.text = $"+${formattedIncome} / user ({lifetimeFormatted})";
        Income.color = Visuals.GetGradientColor(0, 5, income);

        bool isChosen = Marketing.IsCompanyActiveInChannel(Flagship, channel);
        CanvasGroup.alpha = isChosen ? 1 : 0.92f;
        Draw(ChosenImage, isChosen);

        bool isExploredMarket = channel1.ID % 3 > 1;
        if (!isExploredMarket)
        {
            ExplorationImage.fillAmount = (CurrentIntDate % 7) / 7f; // Random.Range(0, 1f);
        }
        else
        {
            ExplorationImage.fillAmount = 0; //
        }
    }

    public void SetEntity(GameEntity channel)
    {
        this.channel = channel;

        ViewRender();
    }
}

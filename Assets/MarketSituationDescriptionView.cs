using Assets.Utils;

public class MarketSituationDescriptionView : UpgradedParameterView
{
    public override string RenderValue()
    {
        int diff = MarketingUtils.GetMarketDiff(GameContext, MyProductEntity.company.Id);

        //"We are out of trends We follow trends We are leading trends!"
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        if (MyProductEntity.isTechnologyLeader)
        {
            Colorize(VisualConstants.COLOR_BEST);
            return "We are best!";
        }

        if (diff == 0)
        {
            Colorize(VisualConstants.COLOR_POSITIVE);
            return "Market fit";
        }

        Colorize(VisualConstants.COLOR_NEGATIVE);
        return $"We are out of market by {diff} levels";
    }

    public override string RenderHint()
    {
        var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        return $"Best app is: {bestApp.product.Name} ({bestApp.product.ProductLevel})" +
            $"\n{NicheUtils.GetProductCompetitivenessBonus(MyProductEntity, GameContext).ToString()}";
    }
}

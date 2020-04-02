using Assets.Core;

// TODO REMOVE
public class MarketSituationDescriptionView : UpgradedParameterView
{
    public override string RenderValue()
    {
        return "";
        //int diff = MarketingUtils.GetMarketDiff(GameContext, MyProductEntity.company.Id);

        ////"We are out of trends We follow trends We are leading trends!"
        //var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        //if (MyProductEntity.isTechnologyLeader)
        //{
        //    Colorize(VisualConstants.COLOR_BEST);
        //    return "We are best!";
        //}

        //if (diff == 0)
        //{
        //    Colorize(VisualConstants.COLOR_POSITIVE);
        //    return "Market fit";
        //}

        //Colorize(VisualConstants.COLOR_NEGATIVE);
        //return $"Out of market by {diff} levels";
    }

    public override string RenderHint()
    {
        //var bestApp = NicheUtils.GetLeaderApp(GameContext, MyProductEntity.company.Id);

        return "";

        //return $"Best app is: {bestApp.company.Name} ({bestApp.product.ProductLevel})" +
        //    $"\n{NicheUtils.GetProductCompetitivenessBonus(MyProductEntity, GameContext).ToString()}";
    }
}

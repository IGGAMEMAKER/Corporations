using Assets.Core;

public class IterationGoal : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        if (!Companies.IsExploredCompany(GameContext, SelectedCompany))
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, GameContext);

        return Products.IsWillInnovate(SelectedCompany, GameContext) ?
            $"Has {chance}% chance to innovate in" :
            "Will upgrade in";
    }
}

using Assets.Utils;

public class IterationGoal : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        if (!CompanyUtils.IsExploredCompany(GameContext, SelectedCompany))
            return "";

        var chance = ProductUtils.GetInnovationChance(SelectedCompany, GameContext);

        return ProductUtils.IsWillInnovate(SelectedCompany, GameContext) ?
            $"Has {chance}% chance to innovate in" :
            "Will upgrade in";
    }
}

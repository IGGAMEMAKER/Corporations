using Assets.Core;

public class IterationGoal : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        if (!Companies.IsExploredCompany(Q, SelectedCompany))
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, Q);

        return Products.IsWillInnovate(SelectedCompany, Q) ?
            $"Has {chance}% chance to innovate in" :
            "Will upgrade in";
    }
}

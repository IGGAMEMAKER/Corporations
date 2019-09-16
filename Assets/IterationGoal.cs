using Assets.Utils;

public class IterationGoal : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ProductUtils.IsWillInnovate(SelectedCompany, GameContext) ?
            "Will innovate in" :
            "Will upgrade in";
    }
}

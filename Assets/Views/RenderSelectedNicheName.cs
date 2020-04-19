using Assets.Core;

public class RenderSelectedNicheName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        return "Market of " + Enums.GetFormattedNicheName(SelectedNiche);
    }
}

using Assets.Core;

public class RenderSelectedNicheName : ParameterView
{
    public override string RenderValue()
    {
        return "Market of " + Enums.GetFormattedNicheName(ScreenUtils.GetSelectedNiche(Q));
    }
}

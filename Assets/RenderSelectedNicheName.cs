using Assets.Core;

public class RenderSelectedNicheName : ParameterView
{
    public override string RenderValue()
    {
        return "Market of " + EnumUtils.GetFormattedNicheName(ScreenUtils.GetSelectedNiche(Q));
    }
}

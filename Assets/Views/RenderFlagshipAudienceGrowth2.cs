using Assets.Core;

public class RenderFlagshipAudienceGrowth2 : ParameterDynamicView
{
    public override long GetParameter()
    {
        Colorize(Colors.COLOR_POSITIVE);

        return Marketing.GetAudienceGrowth(Flagship, Q);
    }

    public override string RenderHint()
    {
        return "WUT?";
    }
}

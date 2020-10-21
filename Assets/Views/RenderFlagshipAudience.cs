using Assets.Core;

public class RenderFlagshipAudience : ParameterDynamicView
{
    public override long GetParameter()
    {
        return Marketing.GetUsers(Flagship);
    }

    public override string RenderHint()
    {
        var bonus = Marketing.GetAudienceChange(Flagship, Q, true);

        return Visuals.Positive(bonus.ToString()); // + "\n" + churnBonus.ToString(true);
    }
}

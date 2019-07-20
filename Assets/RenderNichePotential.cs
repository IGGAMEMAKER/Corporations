using Assets.Utils;

public class RenderNichePotential : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var potential = NicheUtils.GetMarketPotential(GameContext, SelectedNiche);

        return Format.Money(potential);
    }
}

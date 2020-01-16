using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        var features = Products.GetFreeImprovements(SelectedCompany);
        var expGain = Economy.GetIdeas(SelectedCompany, GameContext);

        var gain = expGain / 100f;

        return $"{features} (+{gain})";
    }
}

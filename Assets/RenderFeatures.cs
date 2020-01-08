using Assets.Core;

public class RenderFeatures : ParameterView
{
    public override string RenderValue()
    {
        var features = SelectedCompany.productImprovements.Count;
        var level = Products.GetProductLevel(SelectedCompany);

        var freeFeatures = UnityEngine.Mathf.Max(level - features, 0);

        var freeFeaturesDescription = "";
        if (freeFeatures > 0)
            freeFeaturesDescription = $"(+{freeFeatures})";

        return $"{features} {freeFeaturesDescription}";
    }
}

using Assets.Core;

public class RenderExpertise : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var level = Products.GetProductLevel(SelectedCompany);
        var improvements = SelectedCompany.productImprovements.Count;

        var freeImprovements = level - improvements;

        if (freeImprovements > 0)
            return freeImprovements + "";

        return "---";
    }
}

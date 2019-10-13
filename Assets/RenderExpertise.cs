public class RenderExpertise : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var level = SelectedCompany.expertise.ExpertiseLevel;
        var improvements = SelectedCompany.productImprovements.Count;

        var freeImprovements = level - improvements;

        if (freeImprovements > 0)
            return freeImprovements + " free improvements";

        return level + "LVL";
    }
}

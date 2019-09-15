using Assets.Utils;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        return CompanyUtils.GetInnovationChanceDescription(SelectedCompany).ToString();
    }

    public override string RenderValue()
    {
        return CompanyUtils.GetInnovationChance(SelectedCompany) + "%";
    }
}

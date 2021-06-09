using Assets.Core;

public class RenderFlagshipName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var company = Flagship;

        var color = Colors.COLOR_COMPANY_WHERE_I_AM_CEO;
        
        var status = Enums.GetFormattedCompanyType(company.company.CompanyType);

        return Visuals.Colorize(company.company.Name, color) + $"#{company.creationIndex} ({status})";
    }
}

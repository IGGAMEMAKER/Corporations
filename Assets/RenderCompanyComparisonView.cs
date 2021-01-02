using Assets.Core;

public class RenderCompanyComparisonView : ParameterView
{
    public override string RenderValue()
    {
        var strongerCompanies = Companies.GetCompanyNames(Companies.GetStrongerCompetitors(SelectedCompany, Q, false));
        var weakerCompanies = Companies.GetCompanyNames(Companies.GetWeakerCompetitors(SelectedCompany, Q, false));

        return $"\n\n<b>Stronger</b> than {weakerCompanies}\n\n<b>Weaker</b> than {strongerCompanies}";
    }
}
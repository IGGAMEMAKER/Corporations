using Assets.Utils;

public class CloseCompanyController : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.CloseCompany(GameContext, SelectedCompany);

        var daughters = CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id);

        if (daughters.Length > 0)
        {
            // pick another daughter company
            NavigateToCompany(CurrentScreen, daughters[0].company.Id);
        }
        else
        {
            NavigateToProjectScreen(MyCompany.company.Id);
        }
    }
}

using Assets.Utils;

public class AbandonCompanyController : ButtonController
{
    public override void Execute()
    {
        if (SelectedCompany != null)
        {
            CompanyUtils.LeaveCEOChair(GameContext, SelectedCompany.company.Id);

            //ReNavigate();
        }
    }
}
using Assets.Core;

public class AbandonCompanyController : ButtonController
{
    public override void Execute()
    {
        if (SelectedCompany != null)
        {
            Companies.LeaveCEOChair(Q, SelectedCompany.company.Id);

            //ReNavigate();
        }
    }
}
public class SetSelectedCompanyToMyCompany : ButtonController
{
    public override void Execute()
    {
        NavigateToCompany(CurrentScreen, MyCompany.company.Id);
    }
}

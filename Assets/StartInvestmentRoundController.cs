using Assets.Core;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        Companies.StartInvestmentRound(Q, SelectedCompany.company.Id);

        //ReNavigate();
    }
}

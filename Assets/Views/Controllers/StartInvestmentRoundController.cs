using Assets.Core;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        Companies.StartInvestmentRound(Q, MyCompany.company.Id);

        //ReNavigate();
    }
}

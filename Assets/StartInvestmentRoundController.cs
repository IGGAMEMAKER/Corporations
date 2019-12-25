using Assets.Utils;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        Companies.StartInvestmentRound(GameContext, SelectedCompany.company.Id);

        //ReNavigate();
    }
}

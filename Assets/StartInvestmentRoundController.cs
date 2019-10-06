using Assets.Utils;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.StartInvestmentRound(GameContext, SelectedCompany.company.Id);

        //ReNavigate();
    }
}

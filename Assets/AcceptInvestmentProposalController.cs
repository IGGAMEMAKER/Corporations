using Assets.Utils;

public class AcceptInvestmentProposalController : ButtonController
{
    public int InvestorId;

    public override void Execute()
    {
        Companies.AcceptProposal(GameContext, SelectedCompany.company.Id, InvestorId);
        //ReNavigate();
    }
}

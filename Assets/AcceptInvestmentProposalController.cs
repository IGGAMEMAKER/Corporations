using Assets.Core;

public class AcceptInvestmentProposalController : ButtonController
{
    public int InvestorId;

    public override void Execute()
    {
        Companies.AcceptInvestmentProposal(GameContext, SelectedCompany.company.Id, InvestorId);
        //ReNavigate();
    }
}

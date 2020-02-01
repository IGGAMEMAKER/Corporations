using Assets.Core;

public class RejectInvestmentProposalController : ButtonController
{
    public int InvestorId;

    public override void Execute()
    {
        Companies.RejectProposal(Q, SelectedCompany.company.Id, InvestorId);
        //ReNavigate();
    }
}

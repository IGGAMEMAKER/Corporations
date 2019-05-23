using Assets.Utils;

public class RejectInvestmentProposalController : ButtonController
{
    public int InvestorId;

    public override void Execute()
    {
        CompanyUtils.RejectProposal(GameContext, SelectedCompany.company.Id, InvestorId);
        //ReNavigate();
    }
}

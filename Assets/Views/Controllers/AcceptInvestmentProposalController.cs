using Assets.Core;

public class AcceptInvestmentProposalController : ButtonController
{
    public int InvestorId;

    public override void Execute()
    {
        var investor = Companies.GetInvestorById(Q, InvestorId);

        Companies.AcceptInvestmentProposal(Q, MyCompany, investor);
        Navigate(ScreenMode.InvesmentProposalScreen);
        //ReNavigate();
    }
}

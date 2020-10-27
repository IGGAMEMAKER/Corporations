using Assets;
using Assets.Core;

public class AcceptProposalsButton : ButtonController
{
    public override void Execute()
    {
        var proposalScreen = FindObjectOfType<InestmentProposalScreen>();

        if (proposalScreen.urgency < 2)
        {
            SoundManager.PlayFastCashSound();
        }
        else
        {
            SoundManager.PlayFastCashSound();
            SoundManager.Play(Sound.FillPaper);
        }

        // sets urgency
        proposalScreen.UpdateStartDates();

        Companies.AcceptAllInvestmentProposals(MyCompany, Q);
        MyCompany.RemoveAcceptsInvestments();


        // set goal
        Investments.AddCompanyGoal(MyCompany, Q, proposalScreen.Goal);

        proposalScreen.ResetOffer();
    }
}

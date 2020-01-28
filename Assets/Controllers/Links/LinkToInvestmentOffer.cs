public class LinkToInvestmentOffer : ButtonController
{
    int investorId;

    public override void Execute()
    {
        Navigate(ScreenMode.InvestmentOfferScreen, Balance.MENU_SELECTED_INVESTOR, investorId);
    }

    public void SetInvestorId(int InvestorId)
    {
        investorId = InvestorId;
    }
}

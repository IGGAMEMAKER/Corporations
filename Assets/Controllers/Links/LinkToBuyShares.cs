public class LinkToBuyShares : ButtonController
{
    int investorId;

    public override void Execute()
    {
        Navigate(ScreenMode.BuySharesScreen, Balance.MENU_SELECTED_INVESTOR, investorId);
    }

    public void SetInvestorId(int InvestorId)
    {
        investorId = InvestorId;
    }
}

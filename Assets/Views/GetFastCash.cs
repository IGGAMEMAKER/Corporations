using Assets;
using Assets.Core;
using UnityEngine.UI;

public class GetFastCash : CompanyUpgradeButton
{
    public Text AmountOfLoans;
    public ShareholdersOnMainScreenListView ShareholdersOnMainScreenListView;

    public override void Execute()
    {
        // render cash overflow popup
        if (Economy.IsHasCashOverflow(Q, MyCompany))
        {
            NotificationUtils.AddPopup(Q, new PopupMessageInfo("You have too much cash!", "Spend it first"));
            return;
        }

        SoundManager.Play(Sound.MoneyIncome);
        Economy.RaiseFastCash(Q, MyCompany);

        ShareholdersOnMainScreenListView.RenderShareholderData();
    }

    void RenderAmountOfLoans()
    {
        var valuation = Economy.GetCompanyCost(Q, MyCompany);
        var offer = sum;

        var balance = Economy.BalanceOf(MyCompany);
        var maxCashLimit = valuation * 7 / 100;

        var amountOfLoans = (maxCashLimit - balance) / offer;
        AmountOfLoans.text = amountOfLoans.ToString();
    }

    int fraction = C.FAST_CASH_COMPANY_SHARE;

    long sum => Economy.GetFastCashAmount(Q, MyCompany);

    public override string GetBenefits()
    {
        return ""; // $"Get {Format.Money(sum)}";
        //return $"Get {Format.Money(sum)} for {fraction}% of company";
    }

    public override string GetButtonTitle()
    {
        RenderAmountOfLoans();
        //AmountOfLoans.text = "2";

        return "Demand extra cash";
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}

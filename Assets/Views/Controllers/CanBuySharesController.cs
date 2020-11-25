using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hint))]
public class CanBuySharesController : View
{
    Button Button;
    Hint Hint;

    public override void ViewRender()
    {
        base.ViewRender();

        var selectedInvestor = ScreenUtils.GetSelectedInvestor(Q);

        if (selectedInvestor == null)
            return;

        var investorId = selectedInvestor.shareholder.Id;

        if (!Companies.IsInvestsInCompany(SelectedCompany, selectedInvestor))
            return;

        Render(investorId);
    }

    public bool WillSell(int investorId, int companyId)
    {
        return true; // CompanyUtils.IsAreSharesSellable(GameContext, companyId);
    }

    public void Render(int investorId)
    {
        var company = SelectedCompany;

        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        var investor = Companies.GetInvestorById(Q, investorId);

        var cost = Companies.GetSharesCost(Q, company, investor);

        // TODO we don't always buy companies as Company Group. We can do it as human or investment fund too!
        var have = Economy.BalanceOf(MyGroupEntity);

        bool wantsToSell = WillSell(investorId, company.company.Id);
        bool canAfford = have >= cost;

        Button.interactable = canAfford && cost > 0 && wantsToSell;



        int percentage = Companies.GetShareSize(Q, company, investor);

        var text = $"Buying {percentage}% of shares will cost us ({MyGroupEntity.company.Name}) {Format.Money(cost)}";

        var paymentAbility = canAfford ? Visuals.Positive(Format.Money(have)) : Visuals.Negative(Format.Money(have));

        //var desireToSell = wantsToSell ? ""

        string hint = text + "\n We have " + paymentAbility;

        GetComponent<Hint>().SetHint(hint);
    }
}

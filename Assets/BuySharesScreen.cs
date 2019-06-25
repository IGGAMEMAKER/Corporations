using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class BuySharesScreen : View
{
    [SerializeField] Text CompanyName;
    [SerializeField] Text SharesInfo;
    [SerializeField] Text Valuation;
    [SerializeField] Text Offer;

    [SerializeField] Text ProposalStatus;
    [SerializeField] BuyShares BuyShares;
    [SerializeField] CanBuySharesController CanBuySharesController;

    int MoneyOffer;

    public override void ViewRender()
    {
        base.ViewRender();

        var shareholder = ScreenUtils.GetSelectedInvestor(GameContext);
        var investorId = shareholder.shareholder.Id;

        SharesInfo.text = shareholder.shareholder.Name + " owns " + CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, investorId) + "% of company";

        var shareCost = CompanyUtils.GetSharesCost(GameContext, SelectedCompany.company.Id, investorId);
        Valuation.text = Format.Money(shareCost);

        Offer.text = Format.Money(shareCost);

        CompanyName.text = $"Buy shares of company {SelectedCompany.company.Name}";

        ProposalStatus.text = true ? Visuals.Positive("They will accept our offer!") : Visuals.Negative("They will decline: Wants more money");

        BuyShares.ShareholderId = investorId;
        CanBuySharesController.Render(investorId);
    }
}

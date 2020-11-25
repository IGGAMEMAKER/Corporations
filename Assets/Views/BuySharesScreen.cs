using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class BuySharesScreen : View
{
    public Text CompanyName;
    public Text SharesInfo;
    public Text Valuation;
    public Text Offer;

    public Text ProposalStatus;
    public BuyShares BuyShares;
    public CanBuySharesController CanBuySharesController;

    public override void ViewRender()
    {
        base.ViewRender();

        var shareholder = ScreenUtils.GetSelectedInvestor(Q);

        if (shareholder == null)
            return;

        var investorId = shareholder.shareholder.Id;
        var company = SelectedCompany;

        SharesInfo.text = shareholder.shareholder.Name + " owns " + Companies.GetShareSize(Q, company, shareholder) + "% of company";

        var shareCost = Companies.GetSharesCost(Q, company, shareholder);
        Valuation.text = Format.Money(shareCost);

        Offer.text = Format.Money(shareCost);

        CompanyName.text = $"Buy shares of company {SelectedCompany.company.Name}";

        ProposalStatus.text = true ? Visuals.Positive("They will accept our offer!") : Visuals.Negative("They will decline: Wants more money");

        BuyShares.ShareholderId = investorId;
        CanBuySharesController.Render(investorId);
    }
}

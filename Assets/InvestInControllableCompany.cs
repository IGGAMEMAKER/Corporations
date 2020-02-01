using Assets.Core;
using UnityEngine.UI;

public class InvestInControllableCompany : ButtonController
{
    public int Percent;

    public override void Execute()
    {
        int companyId = SelectedCompany.company.Id;

        var c = MyGroupEntity;

        long Offer = c.companyResource.Resources.money * Percent / 100;

        int shareholderId = MyGroupEntity.shareholder.Id;

        Companies.AddInvestmentProposal(Q, companyId, new InvestmentProposal {
            ShareholderId = shareholderId,
            Valuation = 0,
            Offer = Offer,
            WasAccepted = false,
            InvestorBonus = InvestorBonus.None
        });

        Companies.AcceptInvestmentProposal(Q, companyId, shareholderId);
    }

    public void ResetValue(float value)
    {
        Percent = (int)(value * 100);

        UpdateInteractibility();
    }

    void UpdateInteractibility()
    {
        GetComponent<Button>().interactable = Percent >= 0;
    }
}

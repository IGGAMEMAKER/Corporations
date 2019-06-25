using Assets.Utils;
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

        Render(ScreenUtils.GetSelectedInvestor(GameContext).shareholder.Id);
    }

    public bool WillSell(int investorId, int companyId)
    {
        return CompanyUtils.IsAreSharesSellable(GameContext, companyId);
    }

    public void Render(int investorId)
    {
        int companyId = SelectedCompany.company.Id;

        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        var cost = CompanyUtils.GetSharesCost(GameContext, companyId, investorId);

        // TODO we don't always buy companies as Company Group. We can do it as human or investment fund too!
        var have = MyGroupEntity.companyResource.Resources.money;

        bool wantsToSell = WillSell(investorId, companyId);
        bool canAfford = have >= cost;

        Button.interactable = canAfford && cost > 0 && wantsToSell;

        int percentage = CompanyUtils.GetShareSize(GameContext, companyId, investorId);

        string hint = $"Buying {percentage}% of shares will cost us ({MyGroupEntity.company.Name}) ${cost}\n We have ${have}";

        GetComponent<Hint>().SetHint(hint);
    }
}

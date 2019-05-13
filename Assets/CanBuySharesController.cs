using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CanBuySharesController : View
{
    Button Button;
    Hint Hint;

    public void Render(int investorId)
    {
        int companyId = SelectedCompany.company.Id;

        Button = GetComponent<Button>();
        Hint = GetComponent<Hint>();

        if (MyGroupEntity == null)
        {
            Button.interactable = false;
            Hint.SetHint("To buy shares of this company, promote your company to group of companies");

            return;
        }

        var cost = CompanyUtils.GetSharesCost(GameContext, companyId, investorId);

        // TODO we don't always buy companies as Company Group. We can do it as human or investment fund too!
        var have = MyGroupEntity.companyResource.Resources.money;

        bool availableForSale = CompanyUtils.IsAreSharesSellable(GameContext, companyId);
        bool canAfford = have >= cost;

        Button.interactable = canAfford && availableForSale;

        int percentage = CompanyUtils.GetShareSize(GameContext, companyId, investorId);

        string hint = $"Buying {percentage}% of shares will cost us ({MyGroupEntity.company.Name}) ${cost}\n We have ${have}";

        GetComponent<Hint>().SetHint(hint);
    }
}

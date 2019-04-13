using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CanBuySharesController : View
{
    public void Render(int investorId)
    {
        int companyId = SelectedCompany.company.Id;

        var cost = CompanyUtils.GetSharesCost(GameContext, companyId, investorId);

        Debug.Log($"cost of company {companyId}: {cost}");

        // TODO we don't always buy companies as Company Group. We can do it as human too!
        var have = MyGroupEntity.shareholder.Money;


        GetComponent<Button>().interactable = have >= cost;

        int percentage = CompanyUtils.GetShareSize(GameContext, companyId, investorId);

        string hint = $"Buying {percentage}% of shares will cost us ({MyGroupEntity.company.Name}) ${cost}\n We have ${have}";

        GetComponent<Hint>().SetHint(hint);
        // They don't plan selling their shares
    }
}

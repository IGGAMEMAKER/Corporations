using Assets.Utils;
using UnityEngine.UI;

public class CanBuySharesController : View
{
    void Update()
    {
        int companyId = SelectedCompany.company.Id;
        int investorId = GetComponent<BuyShares>().ShareholderId;

        var cost = CompanyUtils.GetSharesCost(GameContext, companyId, investorId);

        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        // TODO we don't always buy companies as Company Group. We can do it as human too!
        var have = c.shareholder.Money;


        GetComponent<Button>().interactable = have >= cost;

        int percentage = CompanyUtils.GetShareSize(GameContext, companyId, investorId);

        GetComponent<Hint>().SetHint($"Buying {percentage}% of shares will cost us ({MyGroupEntity.company.Name}) ${cost}\n We have ${have}"); // They don't plan selling their shares
    }
}

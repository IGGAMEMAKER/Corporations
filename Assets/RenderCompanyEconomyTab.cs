using Assets.Utils;
using UnityEngine.UI;

public class RenderCompanyEconomyTab : View
{
    public Text Income;
    public Text Maintenance;
    public ColoredValuePositiveOrNegative Change;

    void Render()
    {
        int companyId = GetComponent<SetTargetCompany>().companyId;

        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        Income.text = ValueFormatter.ShortenValueMockup(ProductEconomicsUtils.GetIncome(company)) + "$";
        Maintenance.text = ValueFormatter.ShortenValueMockup(ProductEconomicsUtils.GetMaintenance(company)) + "$";

        Change.UpdateValue(ProductEconomicsUtils.GetBalance(company));
    }

    private void Update()
    {
        Render();
    }
}

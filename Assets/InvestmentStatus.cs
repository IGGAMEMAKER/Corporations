using Assets.Utils;
using UnityEngine.UI;

public class InvestmentStatus : View
{
    private void Update()
    {
        Render();
    }

    void Render()
    {
        bool sellable = CompanyUtils.IsAreSharesSellable(GameContext, SelectedCompany.company.Id);

        string sellablePhrase = sellable ? "" : "We cannot buy shares now! Wait until next investment round or IPO\n\n";

        GetComponent<Text>().text = sellablePhrase;
    }
}

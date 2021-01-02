using Assets.Core;
using UnityEngine.UI;

public class RenderProductKPI : View
{
    public Text Funding;
    public Text Income;
    public Text MarketingBudget;

    public override void ViewRender()
    {
        base.ViewRender();

        var product = SelectedCompany;

        if (!product.hasProduct)
            return;

        var profit = Economy.GetProfit(Q, product, true);

        var funding = Economy.GetFundingBudget(product, Q);
        Funding.text = Visuals.Positive(Format.Money(funding));

        var income = Economy.GetProductIncome(product);
        Income.text = Visuals.Positive(Format.Money(income));

        var marketingBudget = Economy.GetMarketingBudget(product, Q);
        MarketingBudget.text = Visuals.Negative(Format.Money(marketingBudget));
    }
}

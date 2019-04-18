using Assets.Utils;

public class CurrentProductPriceView : ParameterView
    //, IFinanceListener
{
    public override string RenderHint()
    {
        if (MyProductEntity.finance.price == 0)
            return "This product is FREE. Increase prices to start making money!";

        return "Base price: " + CompanyEconomyUtils.GetBaseProductPrice(MyProductEntity)
            + "$ \n]nWe increased price by" + (MyProductEntity.finance.price - 1) * 10 + "%";
    }

    public override string RenderValue()
    {
        return CompanyEconomyUtils.GetProductPrice(MyProductEntity) + "$";
    }
}

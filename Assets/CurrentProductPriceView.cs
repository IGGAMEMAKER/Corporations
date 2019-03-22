using Assets.Utils;

public class CurrentProductPriceView : ParameterView
{
    public override string RenderHint()
    {
        if (myProductEntity.finance.price == 0)
            return "This product is FREE. Increase prices to start making money!";

        return "Base price: " + ProductEconomicsUtils.GetBasePrice(myProductEntity) + "$ \n]nWe increased price by" + (myProductEntity.finance.price - 1) * 10 + "%";
    }

    public override string RenderValue()
    {
        return ProductEconomicsUtils.GetProductPrice(myProductEntity) + "$";
    }
}

using Assets.Utils;

public class CurrentProductPriceView : ParameterView
    //, IFinanceListener
{
    public override string RenderHint()
    {
        if (MyProductEntity.finance.price == 0)
            return "This product is FREE. Increase prices to start making money!";

        return "Base price: " + ProductEconomicsUtils.GetBasePrice(MyProductEntity) + "$ \n]nWe increased price by" + (MyProductEntity.finance.price - 1) * 10 + "%";
    }

    public override string RenderValue()
    {
        return ProductEconomicsUtils.GetProductPrice(MyProductEntity) + "$";
    }

    //void OnEnable()
    //{
    //    MyProductEntity.AddFinanceListener(this);

    //    Render();
    //}

    //void OnDisable()
    //{
    //    MyProductEntity.RemoveFinanceListener(this);
    //}

    //void IFinanceListener.OnFinance(GameEntity entity, int price, int marketingFinancing, int salaries, float basePrice)
    //{
    //    Render();
    //}
}

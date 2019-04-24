using Assets.Utils;
using System.Text;

public class CurrentProductPriceView : ParameterView
    //, IFinanceListener
{
    public override string RenderHint()
    {
        if (IsFree)
            return "This product is FREE. Increase prices to start making money!";

        int price = (MyProductEntity.finance.price - 1) * 10;

        StringBuilder text = new StringBuilder();

        text.AppendFormat("Base price: {0}$ \n\nWe increased price by {1}%", CompanyEconomyUtils.GetBaseProductPrice(MyProductEntity), price);

        return text.ToString();
    }

    bool IsFree
    {
        get
        {
            return MyProductEntity.finance.price == 0;
        }
    }

    public override string RenderValue()
    {
        if (IsFree)
            return "Free";

        return CompanyEconomyUtils.GetProductPrice(MyProductEntity) + "$";
    }
}

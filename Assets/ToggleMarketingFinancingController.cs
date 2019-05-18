using Assets.Utils;

public class ToggleMarketingFinancingController : ButtonController
    //, IFinanceListener
{
    public MarketingFinancing marketingFinancing;

    public override void Execute()
    {
        MarketingUtils.SetFinancing(GameContext, MyProductEntity.company.Id, marketingFinancing);
    }




    //void OnEnable()
    //{
    //    MyProductEntity.AddFinanceListener(this);

    //    Render();
    //}

    //void Render()
    //{
    //    ToggleIsChosenComponent(MyProductEntity.finance.marketingFinancing == marketingFinancing);
    //}

    //void IFinanceListener.OnFinance(GameEntity entity, Pricing price, MarketingFinancing marketingFinancing, int salaries, float basePrice)
    //{
    //    Render();
    //}
}

public class ListenProductFinancingChanges : Controller
    , IFinanceListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddFinanceListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveFinanceListener(this);
    }

    void IFinanceListener.OnFinance(GameEntity entity, Pricing price, MarketingFinancing marketingFinancing, int salaries, float basePrice)
    {
        Render();
    }
}

public abstract partial class ButtonController
{
    public void TriggerEventUpgradeProduct(int productId, int ProductLevel)
    {
        MyProductEntity.AddEventUpgradeProduct(productId, ProductLevel);
    }

    public void TriggerEventTargetingToggle(int productId)
    {
        MyProductEntity.AddEventMarketingEnableTargeting(productId);
    }

    public void TriggerEventSetPrice(int productId, Pricing level)
    {
        MyProductEntity.AddEventFinancePricingChange(productId, level);
    }

    public GameEntity ListenProductChanges()
    {
        return MyProductEntity;
    }
}

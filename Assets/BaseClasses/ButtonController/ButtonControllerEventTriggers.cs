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

    public void TriggerEventSetPrice(int productId, int level)
    {
        MyProductEntity.AddEventFinancePricingChange(productId, level);
    }
}

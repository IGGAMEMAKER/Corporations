using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;


public interface IEventGenerator
{
    void TriggerEventUpgradeProduct(int productId, int ProductLevel);
    void TriggerEventTargetingToggle(int productId);
}

public abstract partial class ButtonController : IEventGenerator
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

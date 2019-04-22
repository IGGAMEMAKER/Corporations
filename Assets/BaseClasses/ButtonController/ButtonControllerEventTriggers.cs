using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract partial class ButtonController : MonoBehaviour, IEventGenerator
{
    public void TriggerEventUpgradeProduct(int productId, int ProductLevel)
    {
        MyProductEntity.AddEventUpgradeProduct(productId, ProductLevel);
    }

    public void TriggerEventTargetingToggle(int productId)
    {
        MyProductEntity.AddEventMarketingEnableTargeting(productId);
    }

    public void TriggerEventIncreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, 1);
    }

    public void TriggerEventDecreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, -1);
    }

    void TriggerEventChangePrice(int productId, int change)
    {
        int price = MyProductEntity.finance.price;

        MyProductEntity.AddEventFinancePricingChange(productId, price, change);
    }
}

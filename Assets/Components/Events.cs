using Entitas;

public interface IEventGenerator
{
    void TriggerEventUpgradeProduct(int productId, int ProductLevel);
    void TriggerEventTargetingToggle(int productId);
}


[Game]
public class EventUpgradeProductComponent : IComponent
{
    public int productId;
    public int previousLevel;
}

public class EventFinancePricingChangeComponent : IComponent
{
    public int productId;
    public int level;
    public int change;
}

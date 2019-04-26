using Entitas;

[Game]
public class EventUpgradeProductComponent : IComponent
{
    public int productId;
    public int previousLevel;
}

public class EventFinancePricingChangeComponent : IComponent
{
    public int productId;
    public Pricing level;
}

using Entitas;

[Game]
public class EventComponent : IComponent { }

#region Product specific events

[Game]
public class EventUpgradeProductComponent : IComponent
{
    public int productId;
    public int previousLevel;
}

[Game]
public class EventUpgradeAnalyticsComponent : IComponent
{
    public int productId;
}

[Game]
public class EventMarketingEnableTargetingComponent : IComponent
{
    public int productId;
}

[Game]
public class EventStaffHireProgrammerComponent : IComponent
{
    public int productId;
}

#endregion

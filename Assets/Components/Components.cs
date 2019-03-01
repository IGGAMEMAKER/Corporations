using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public enum Niche
{
    SocialNetwork,
    Messenger
}

public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

[Game]
public struct ControlledByPlayerComponent : IComponent {}

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
    public float x;
    public float y;
}

public class TaskComponent: IComponent
{
    public TaskType TaskType;
    public int StartTime;
    public int Duration;
    public int EndTime;
    //public TeamResource ResourcesSpent;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent: IComponent
{
    public int Id;
    public string Name;
    public Niche Niche;

    public int ProductLevel;
    public int ExplorationLevel;

    public TeamResource Resources;
}

[Game, Event(EventTarget.Self)]
public class AnalyticsComponent: IComponent
{
    public int Analytics;
    public int ExperimentCount;
}

[Game, Event(EventTarget.Self)]
public class TeamComponent: IComponent
{
    public int Programmers;
    public int Managers;
    public int Marketers;

    public int Morale;
}

[Game, Event(EventTarget.Self)]
public class MarketingComponent : IComponent
{
    public uint Clients;
    public int BrandPower;
}

[Game]
public class DebugMessageComponent : IComponent
{
    public string message;
}

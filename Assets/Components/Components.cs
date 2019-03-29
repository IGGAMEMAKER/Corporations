using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

public enum Niche
{
    SocialNetwork,
    Messenger
}

public enum Industry
{
    Communications
}

public enum CompanyType
{
    Product,

    FinancialGroup,

    Group,
    Holding,
    Corporation
}

public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

[Game, Unique]
public class DateComponent : IComponent
{
    public int Date;
}

// only entity - Player can be CEO of only one company at time
[Game]
public struct ControlledByPlayerComponent : IComponent {

}

[Game]
public struct SelectedComponent : IComponent {

}

[Game]
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}

// only entity
[Game]
public struct SelectedCompanyComponent : IComponent {

}



//public interface ITaskManager
//{
//    void AddTask();
//    void 
//}

[Game]
public class TaskManagerComponent : IComponent
{
    public List<GameEntity> Tasks;
}

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
    public float x;
    public float y;
}

public enum ScreenMode
{
    DevelopmentScreen,
    MarketingScreen,
    ProjectScreen,
    BusinessScreen,
    TeamScreen,
    StatsScreen,
    InvesmentsScreen,
    MarketScreen
}

public class MenuComponent : IComponent
{
    public ScreenMode ScreenMode;
}

public class TaskComponent: IComponent
{
    public bool isCompleted;
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
    public Industry Industry;

    public int ProductLevel;
    public int ExplorationLevel;

    public TeamResource Resources;
}

[Game, Event(EventTarget.Self)]
public class FinanceComponent: IComponent
{
    public int price;
    public int marketingFinancing;
    public int salaries;
    public float basePrice;
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

    public bool isTargetingEnabled;
}

[Game, Event(EventTarget.Self)]
public class TargetingComponent : IComponent
{

}

[Game]
public class DebugMessageComponent : IComponent
{
    public string message;
}

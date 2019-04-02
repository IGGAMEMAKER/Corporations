using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

public enum Niche
{
    SocialNetwork,
    Messenger,

    SearchEngine,

    OSCommonPurpose,
    OSSciencePurpose,
}

public enum Industry
{
    Communications,
    Search,
    OS,
    Clouds
}

public enum CompanyType
{
    ProductCompany,

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
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}


[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;

    // skillset, character and perks later
}

[Game]
public class RuledByComponent : IComponent
{
    public HumanComponent human;
    public int CompanyId;
}

// is attached to CompanyComponent
[Game]
public class ShareholdersComponent : IComponent
{
    public Dictionary<int, int> Shareholders; // investorId => amountOfShares
}

public class ShareholderComponent : IComponent
{
    public int Id;
    public string Name;
    public long Money;
}

[Game]
public class NavigationHistoryComponent : IComponent
{

}

// only entity
[Game]
public struct SelectedCompanyComponent : IComponent {

}


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

using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

[Game, Unique, Event(EventTarget.Any)]
public class DateComponent : IComponent
{
    public int Date;
}


[Game, Event(EventTarget.Self)]
public class MenuComponent : IComponent
{
    public ScreenMode ScreenMode;
    public object Data;
}

// only entity
[Game]
public class NavigationHistoryComponent : IComponent
{
    public List<MenuComponent> Queries;
}


[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;

    // skillset, character and perks later
}


public class ShareholderComponent : IComponent
{
    public int Id;
    public string Name;
    public InvestorType InvestorType;
}

public class InvestmentProposal
{
    public int ShareholderId;
    public long Valuation;
    public long Offer;
}

public struct MetricsInfo
{
    public int Date;
    public long Income;
    public long Valuation;
    
    // balance change
    public long Profit;
    public long AudienceSize;
}

[Game]
public class MetricsHistoryComponent : IComponent
{
    public List<MetricsInfo> Metrics;
}


[Game]
public class TaskManagerComponent : IComponent
{
    public List<GameEntity> Tasks;
}

public class TaskComponent: IComponent
{
    public bool isCompleted;
    public TaskType TaskType;
    public int StartTime;
    public int Duration;
    public int EndTime;
}

[Game, Event(EventTarget.Self)]
public class FinanceComponent : IComponent
{
    public Pricing price;
    public MarketingFinancing marketingFinancing;
    public int salaries;
    public float basePrice;
}
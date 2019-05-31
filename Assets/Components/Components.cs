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
    public Dictionary<string, object> Data;
}

// only entity
[Game]
public class NavigationHistoryComponent : IComponent
{
    public List<MenuComponent> Queries;
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

[Game]
public class TimerRunningComponent : IComponent { }



[Game]
public class TutorialComponent : IComponent
{
    public Dictionary<TutorialFunctionality, bool> progress;
}
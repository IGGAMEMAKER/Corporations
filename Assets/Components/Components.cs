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
    public int Speed;
}

[Game]
public class TimerRunningComponent : IComponent { }

[Game, Unique, Event(EventTarget.Any)]
public class TargetDateComponent : IComponent
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

public enum InvestorBonus
{
    None,
    Expertise,
    Branding,
}

public class InvestmentProposal
{
    public int ShareholderId;
    public long Valuation;
    public long Offer;
    public InvestorBonus InvestorBonus;

    public bool WasAccepted;
}


[Game, Event(EventTarget.Self)]
public class TutorialComponent : IComponent
{
    public Dictionary<TutorialFunctionality, bool> progress;
}

[Game, Event(EventTarget.Self)]
public class EventContainerComponent : IComponent
{
    public Dictionary<string, bool> progress;
}

[Game]
public class TestComponent : IComponent
{
    public Dictionary<LogTypes, bool> logs;
}

public enum LogTypes
{
    MyProductCompany,
    MyProductCompanyCompetitors
}

[Game]
public class ResearchComponent : IComponent
{
    public int Level;
}

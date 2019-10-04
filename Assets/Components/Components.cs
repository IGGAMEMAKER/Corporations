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



public enum PopupType
{
    CloseCompany,
    MarketChanges
}

public class PopupMessage
{
    public PopupType PopupType;
}

public class PopupMessageCloseCompany : PopupMessage
{
    public int companyId;

    public PopupMessageCloseCompany (int companyId)
    {
        this.companyId = companyId;
        PopupType = PopupType.CloseCompany;
    }
}

public class PopupMessageMarketPhaseChange : PopupMessage
{
    public NicheType NicheType;

    public PopupMessageMarketPhaseChange(NicheType niche)
    {
        this.NicheType = niche;
        PopupType = PopupType.MarketChanges;
    }
}

public class PopupComponent : IComponent
{
    public List<PopupMessage> PopupMessages;
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

[Game]
public class TaskManagerComponent : IComponent
{
    public List<GameEntity> Tasks;
}

public class TaskComponent: IComponent
{
    public bool isCompleted;
    //public TaskType TaskType;
    //public CompanyTaskType TaskType;
    public CompanyTask CompanyTask;
    public int StartTime;
    public int Duration;
    public int EndTime;
}

[Game]
public class TimerRunningComponent : IComponent { }

[Game]
public class CooldownContainerComponent: IComponent
{
    public Dictionary<string, Cooldown> Cooldowns;
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

[Game]
public class ResearchComponent : IComponent
{
    public int Level;
}

public enum LogTypes
{
    MyProductCompany,
    MyProductCompanyCompetitors
}
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
    MarketChanges,

    BankruptCompany,
    NewCompany,
    TargetInterception,

    InspirationToOpenMarket
}

public class PopupMessage
{
    public PopupType PopupType;
}

public class PopupMessageCompanyClose : PopupMessage
{
    public int companyId;

    public PopupMessageCompanyClose (int companyId)
    {
        this.companyId = companyId;
        PopupType = PopupType.CloseCompany;
    }
}

public class PopupMessageCompanyBankrupt : PopupMessageCompanyEvent
{
    public PopupMessageCompanyBankrupt(int companyId) : base(companyId, PopupType.BankruptCompany) { }
}

public class PopupMessageCompetitorWantsToInterceptOurTargetCompany : PopupMessageCompanyEvent
{
    public int InterceptorCompanyId;
    public PopupMessageCompetitorWantsToInterceptOurTargetCompany(int targetCompanyId, int interceptorId) : base(targetCompanyId, PopupType.TargetInterception) {
        InterceptorCompanyId = interceptorId;
    }
}

public class PopupMessageCompanySpawn : PopupMessageCompanyEvent
{
    public PopupMessageCompanySpawn(int companyId) : base(companyId, PopupType.NewCompany) { }
}

// market state changes
public class PopupMessageMarketPhaseChange : PopupMessageMarketChanges
{
    public PopupMessageMarketPhaseChange(NicheType niche) : base(niche, PopupType.MarketChanges) {}
}
public class PopupMessageMarketInspiration : PopupMessageMarketChanges
{
    public PopupMessageMarketInspiration(NicheType niche) : base(niche, PopupType.InspirationToOpenMarket) {}
}

// universal
public abstract class PopupMessageCompanyEvent : PopupMessage
{
    public int companyId;

    public PopupMessageCompanyEvent(int companyId, PopupType popupType)
    {
        this.companyId = companyId;
        PopupType = popupType;
    }
}

public abstract class PopupMessageMarketChanges : PopupMessage
{
    public NicheType NicheType;

    public PopupMessageMarketChanges(NicheType niche, PopupType popupType)
    {
        this.NicheType = niche;
        PopupType = popupType;
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
using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

public enum NicheType
{
    None,

    SocialNetwork,
    Messenger,

    SearchEngine,

    CloudComputing,

    OSCommonPurpose,
    OSSciencePurpose,
}

public enum IndustryType
{
    Communications,
    Search,
    OS,
    CloudComputing
}

public enum CompanyType
{
    ProductCompany,

    FinancialGroup,

    Group,
    Holding,
    Corporation
}

public enum ScreenMode
{
    DevelopmentScreen,
    MarketingScreen,
    ProjectScreen,
    BusinessScreen,
    TeamScreen,
    StatsScreen,
    CharacterScreen,
    GroupManagementScreen,
    InvesmentsScreen,
    InvesmentProposalScreen,
    IndustryScreen,
    NicheScreen
}

public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

[Game, Unique, Event(EventTarget.Any)]
public class DateComponent : IComponent
{
    public int Date;
}

// Player can be CEO of only one product and one company group at time
[Game]
public struct ControlledByPlayerComponent : IComponent {

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

// only entity
[Game]
public struct SelectedCompanyComponent : IComponent
{

}

//[Game]
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}

[Game]
public class IndustryComponent : IComponent
{
    public IndustryType IndustryType;
}


public struct MarketCompatibility
{
    public NicheType NicheType;
    public int Compatibility;
}

[Game]
public class NicheComponent : IComponent
{
    public NicheType NicheType;
    public IndustryType IndustryType;
    public List<MarketCompatibility> MarketCompatibilities;
    public List<NicheType> CompetingNiches;
    public NicheType Parent;
    public int OpenDate;
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
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class ShareholdersComponent : IComponent
{
    // investorId => amountOfShares
    public Dictionary<int, int> Shareholders;
}

public class ShareholderComponent : IComponent
{
    public int Id;
    public string Name;
    public long Money;
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
    public long Profit; // balance change
    public uint AudienceSize;
}

[Game]
public class MetricsHistoryComponent : IComponent
{
    public List<MetricsInfo> Metrics;
}

[Game]
public class InvestmentProposalsComponent : IComponent
{
    public List<InvestmentProposal> Proposals;
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
public class ProductComponent: IComponent
{
    public int Id;
    public string Name;
    public NicheType Niche;
    //public IndustryType Industry;

    public int ProductLevel;
    //public int ExplorationLevel;

    //public TeamResource Resources;
}

[Game, Event(EventTarget.Self)]
public class CompanyResourceComponent : IComponent
{
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
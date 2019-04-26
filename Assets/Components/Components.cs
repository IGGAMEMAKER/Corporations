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
    DevelopmentScreen = 0,
    MarketingScreen = 1,
    ProjectScreen = 2,
    //BusinessScreen,
    TeamScreen = 3,
    StatsScreen = 4,
    CharacterScreen = 5,
    GroupManagementScreen = 6,
    InvesmentsScreen = 7,
    InvesmentProposalScreen = 8,
    IndustryScreen = 9,
    NicheScreen = 10
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

[Game]
public class FollowingComponent : IComponent { }

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

    // cross promotions
    public List<MarketCompatibility> MarketCompatibilities;
    public List<NicheType> CompetingNiches;

    public NicheType Parent;
    public int OpenDate;

    public float BasePrice;
}

[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;

    // skillset, character and perks later
}

public class CEOComponent : IComponent
{
    // if you fail investor tasks you lose reputation
    public float Reputation;
    public int HumanId;
}

public enum InvestorType
{
    // both value long term solutions
    // aka team effeciency
    // product quality
    // branding
    // Founder - human with strategic goals, Strategic - company with strategic goals
    Founder,
    Strategic,

    // gives less money and wants more investors to come (aka rounds)
    VentureInvestor,

    // wants dividends
    SmallInvestor
}

public enum InvestorGoal
{
    GrowCompanyCost,
    GrowProfit,
    //GrowClientBase,

    //ImproveManagement,
    //ImproveBranding
}

public enum InvestmentRound
{
    Preseed,
    Seed,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J
}

// is attached to CompanyComponent
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class ShareholdersComponent : IComponent
{
    // investorId => amountOfShares
    public Dictionary<int, int> Shareholders;
    // investorId => goal
    public Dictionary<int, InvestorGoal> Goals;
}

// is IPOed
// in future you will be able to switch public/private whenever you want
public class PublicCompanyComponent : IComponent {}

public class AcceptsInvestmentsComponent : IComponent
{
    // set this to 60
    // and decrement everyday
    // once it hits 0 - deactivate
    public int DaysLeft;
}

public class InvestmentRoundsComponent : IComponent
{
    public InvestmentRound InvestmentRound;
}


// if Founder + investment companies shares > Group/Holding/Corp
// or Groups have less than 25%

// if independent, you can promote prouct company to group
// only independent companies can IPO
public class IndependentCompanyComponent : IComponent { }

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
    public long Profit; // balance change
    public long AudienceSize;
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

// social network
// messenger
// blogs

public enum UserType
{
    Newbie,
    Regular,
    Core
}

[Game, Event(EventTarget.Self)]
public class ProductComponent: IComponent
{
    public int Id;
    public string Name;
    public NicheType Niche;

    // platform level
    public int ProductLevel;
    public int ImprovementPoints;

    public Dictionary<UserType, int> Segments;
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
    public long BrandPower;

    public Dictionary<UserType, long> Segments;
}

[Game]
public class TargetUserTypeComponent : IComponent
{
    public UserType UserType;
}

[Game, Event(EventTarget.Self)]
public class TargetingComponent : IComponent
{

}
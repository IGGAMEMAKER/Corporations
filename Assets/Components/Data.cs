
public enum TutorialFunctionality
{
    MarketingMenu,
    CompetitorView,
    PossibleInvestors,
    LinkToProjectViewInInvestmentRounds,
    FirstAdCampaign,

    GoalFirstUsers,
    GoalPrototype,

    GoalBecomeMarketFit,
    GoalRelease,

    GoalBecomeProfitable,

    IPO,

    NeverShow,

    CompletedFirstGoal,

    ClickOnRaiseMoneyLink,
    ClickOnDevelopmentLink,
    ClickOnGroupLink,
}

public struct ProductCompanyResult
{
    public long clientChange;
    public float MarketShareChange;
    public ConceptStatus ConceptStatus;
    public int CompanyId;
}


public enum Risk
{
    Guaranteed,
    Risky,
    TooRisky
}


public enum WorkerRole
{
    // base
    Programmer,
    // base
    Manager,
    // base
    Marketer,

    ProductManager,
    ProjectManager,

    TeamLead,
    MarketingLead,

    TechDirector,
    MarketingDirector,

    // base
    CEO,


    Universal,
}
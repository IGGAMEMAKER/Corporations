using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}

public struct BlockOfShares
{
    public int amount;

    public InvestorType InvestorType;

    public int shareholderLoyalty;
}

public class AliveComponent : IComponent { }
public class OnSalesComponent : IComponent { }

// is attached to CompanyComponent
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class ShareholdersComponent : IComponent
{
    // investorId => amountOfShares
    public Dictionary<int, BlockOfShares> Shareholders;
}

// is IPOed
// in future you will be able to switch public/private whenever you want
public class PublicCompanyComponent : IComponent { }

// groups, holdings, corporations
// excluding products and financial groups
public class ManagingCompanyComponent : IComponent { }


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


// Independent company = has no managing companies in shareholder list
// if independent, you can promote prouct company to group
// only independent companies can IPO
public class IndependentCompanyComponent : IComponent { }


[Game, Event(EventTarget.Self)]
public class CompanyResourceComponent : IComponent
{
    public TeamResource Resources;
}

public class Team
{
    public int Programmers;
    public int Managers;
    public int Marketers;

    public int Morale;
}

public enum TeamStatus
{
    Solo,
    Pair,
    SmallTeam,
    Department,
    BigTeam
}

[Game, Event(EventTarget.Self)]
public class TeamComponent : IComponent
{
    public int Morale;

    public Dictionary<int, WorkerRole> Workers;

    public TeamStatus TeamStatus;
}

[Game]
public class InvestmentProposalsComponent : IComponent
{
    public List<InvestmentProposal> Proposals;
}

[Game]
public class FollowingComponent : IComponent { }


// Player can be CEO of only one product and one company group at time
[Game]
public struct ControlledByPlayerComponent : IComponent { }

public class CEOComponent : IComponent
{
    // if you fail investor tasks you lose reputation
    public float Reputation;
    public int HumanId;
}

public class CooldownsComponent : IComponent
{
    public List<Cooldown> Cooldowns;
}

[Game, Event(EventTarget.Any), Event(EventTarget.Self)]
public class CompanyGoalComponent : IComponent
{
    public InvestorGoal InvestorGoal;

    public long MeasurableGoal;
}

[Game]
public class CompanyFocusComponent : IComponent
{
    public List<NicheType> Niches;
    public List<IndustryType> Industries;
}

public class FollowComponent : IComponent
{
    public List<int> Companies;
    public List<int> Humans;
}

public class CompanyPhaseComponent : IComponent
{
    public bool becameMarketFit;
    public bool becameProfitable;
    public bool startedCorporativeChanges;
    public bool raisedInvestments;
    public bool IPOed;
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

public struct ReportData
{
    public long Cost;
    public int ShareholderId;
    public int position;
}

public struct AnnualReport
{
    public List<ReportData> People;
    public List<ReportData> Groups;
    public List<ReportData> Products;
    public int Date;
}


[Game]
public class MetricsHistoryComponent : IComponent
{
    public List<MetricsInfo> Metrics;
}

public class ReportsComponent : IComponent
{
    public List<AnnualReport> AnnualReports;
}



public class AcquisitionConditions
{
    //public long SellerPrice;
    public long Price;

    public long ByCash;
    public int ByShares; // part of our company
    // to be accepted, byCash + ByShares * BuyerCompanyCost must be greater than SellerPrice

    public bool KeepLeaderAsCEO;
}

public enum AcquisitionTurn
{
    Buyer,
    Seller
}

public class AcquisitionOfferComponent : IComponent
{
    public int CompanyId; // target
    public int BuyerId;

    public AcquisitionTurn Turn;

    public AcquisitionConditions BuyerOffer;
    public AcquisitionConditions SellerOffer;
}

public class PreviousAcquisitionOffersComponent : IComponent
{
    public AcquisitionConditions BuyerOffer;
    public AcquisitionConditions SellerOffer;
}

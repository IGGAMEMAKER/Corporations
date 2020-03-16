using Assets.Core;
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

public class WantsToExpand : IComponent { }

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

public class FlagshipComponent : IComponent { }

public class CEOComponent : IComponent
{
    // if you fail investor tasks you lose reputation
    public float Reputation;
    public int HumanId;
}

//public class CooldownsComponent : IComponent
//{
//    public List<Cooldown> Cooldowns;
//}

[Game, Event(EventTarget.Any), Event(EventTarget.Self)]
public class CompanyGoalComponent : IComponent
{
    public InvestorGoal InvestorGoal;

    public long MeasurableGoal;
}

// only independent companies have that
public class PartnershipsComponent : IComponent
{
    public List<int> companies;
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
    Seller,
    Neutral
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




public enum CorporatePolicy
{
    LeaderOrTeam, // team vs leader
    BuyOrCreate,
    InnovationOrStability, // engineer vs researcher
    FocusingOrSpread,
    SalariesLowOrHigh, // low vs high

    CompetitionOrSupport
}

[Game]
public class CorporateCultureComponent : IComponent
{
    public Dictionary<CorporatePolicy, int> Culture;
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

    public int Organisation;

    public Dictionary<int, WorkerRole> Managers;
    public Dictionary<WorkerRole, int> Workers;

    public TeamStatus TeamStatus;
}

public class EmployeeComponent : IComponent
{
    public Dictionary<int, WorkerRole> Managers;
}

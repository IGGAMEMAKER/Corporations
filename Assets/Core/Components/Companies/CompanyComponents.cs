using Assets.Core;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using System.Text;

public class CompanyHolding
{
    // shares percent
    public int control;

    // controlled company id
    //public int companyId;
    public GameEntity company;

    public List<CompanyHolding> holdings;
}

[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public struct CompanyComponent : IComponent
{
    public int Id;
    public string Name;
    public CompanyType CompanyType;
}

public class AutomaticInvestmentsComponent : IComponent { }

public struct BlockOfShares
{
    public int amount;

    public InvestorType InvestorType;

    public int shareholderLoyalty;

    public List<Investment> Investments;
}

public class AliveComponent : IComponent { }
public class BankruptComponent : IComponent { }
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

public class ResourceTransaction
{
    public string Tag;
    public long TeamResource;
    public int Date;

    public string Print()
    {
        return ScheduleUtils.GetFormattedDate(Date) + " " + Tag + ": " + Visuals.PositiveOrNegativeMinified(TeamResource);
    }
}

public class CompanyResourceHistoryComponent : IComponent
{
    public List<ResourceTransaction> Actions;
}

public class LoggingComponent : IComponent
{
    public List<string> Logs;
}
[Game]
public class ProfilingComponent : IComponent
{
    public long ProfilerMilliseconds;
    public StringBuilder MyProfiler;

    public Dictionary<string, long> Tags;
}

[Game]
public class InvestmentProposalsComponent : IComponent
{
    public List<InvestmentProposal> Proposals;
}

public class CompletedGoalsComponent: IComponent
{
    public List<InvestorGoalType> Goals;
}

public enum CompanyGrowthStyle
{
    None,

    StepByStep,
    Aggressive
}

public enum InvestorInterest
{
    None,

    SellOut,
    BuyBack,
    IPO,
    Dividends
}

public enum VotingStyle
{
    None,
    Percent50,
    Percent75,
    Percent90,
    Percent100
}

[Game]
public class InvestmentStrategyComponent : IComponent
{
    public CompanyGrowthStyle GrowthStyle;
    public VotingStyle VotingStyle;
    public InvestorInterest InvestorInterest;
}

[Game]
public class FollowingComponent : IComponent { }


// Player can be CEO of only one product and one company group at time
[Game]
public struct ControlledByPlayerComponent : IComponent { }
public class RelatedToPlayerComponent : IComponent { }

// PLAYER ONLY
public class FlagshipComponent : IComponent { }

public class CEOComponent : IComponent
{
    // if you fail investor tasks you lose reputation
    public float Reputation;
    public int HumanId;
}

public class PseudoHumanComponent : IComponent
{
    public int RealHumanId;
}

//public class CooldownsComponent : IComponent
//{
//    public List<Cooldown> Cooldowns;
//}

[Game, Event(EventTarget.Any), Event(EventTarget.Self)]
public class CompanyGoalComponent : IComponent
{
    public List<InvestmentGoal> Goals;
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

    public bool IsBetterThan(AcquisitionConditions offer)
    {
        return this.Price >= offer.Price;
    }
}

public enum AcquisitionTurn
{
    Buyer,
    Seller,
    None
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
    // upgrade
    //BuyOrCreate, // BuyOrCreate
    Make,
    Sell,
    DoOrDelegate,

    // choose
    FocusingOrSpread, // just for funds
    SalariesLowOrHigh, // low vs high

    CompetitionOrSupport,

    PeopleOrProcesses,
    DecisionsManagerOrTeam,

    HardSkillsOrSoftSkills
}

[Game]
public class CorporateCultureComponent : IComponent
{
    public Dictionary<CorporatePolicy, int> Culture;
}

public class JobOffer
{
    public long Salary;

    public JobOffer()
    {

    }
    public JobOffer(long salary)
    {
        this.Salary = salary;
    }
}

[Game, Event(EventTarget.Self)]
public class TeamComponent : IComponent
{
    public int Morale;

    public int Organisation;

    public Dictionary<int, WorkerRole> Managers;
    public Dictionary<WorkerRole, int> Workers;

    public List<TeamInfo> Teams;

    public long Salaries;
}
public enum ManagerTask
{
    None,
    Investments,
    Documentation,
    Organisation,
    ImproveAtmosphere,
    Recruiting,

    Polishing,
    ViralSpread
}

public class TeamEfficiency
{
    public int DevelopmentEfficiency;
    public int MarketingEfficiency;

    public float FeatureCap;
    public float FeatureGain;

    public bool isUniqueCompany;
    public int Competitiveness;
}

public class TeamEfficiencyComponent : IComponent
{
    public TeamEfficiency Efficiency;
}

public class TeamInfo
{
    public TeamType TeamType;
    public string Name;

    public List<TeamTask> Tasks;

    //public Dictionary<int, JobOffer> Offers;
    public List<int> Managers;
    public Dictionary<int, WorkerRole> Roles;

    public int Workers;
    public int HiringProgress;

    public List<ManagerTask> ManagerTasks;
    public float Organisation;

    public int ID;
    public bool TooManyLeaders = false;

    // upgrades are written here
    public TeamRank Rank;

    public bool isCoreTeam => ID == 0;
    public bool isFullTeam => Teams.IsFullTeam(this);
}

public enum TeamType
{
    DevelopmentTeam,
    MarketingTeam,

    CrossfunctionalTeam,

    MergeAndAcquisitionTeam,
    SupportTeam,
    ServersideTeam
}

public enum TeamRank
{
    Solo,       // 1    - 1 * 4
    SmallTeam,  // 5    - 2 * 4
    BigTeam,    // 20   - 4 * 4
    Department, // 100  - 10 * 4
}

public class TeamTask
{
    public bool IsPending;

    public int EndDate = -1;
    public int StartDate = -1;

    public bool IsFeatureUpgrade => this is TeamTaskFeatureUpgrade;
    public bool IsMarketingTask => this is TeamTaskChannelActivity;
    public bool IsSupportTask => this is TeamTaskSupportFeature && !(((TeamTaskSupportFeature) this).SupportFeature.SupportBonus is SupportBonusHighload);
    public bool IsHighloadTask => this is TeamTaskSupportFeature && ((TeamTaskSupportFeature) this).SupportFeature.SupportBonus is SupportBonusHighload;

    public bool AreSameTasks(TeamTask t)
    {
        if (!AreSameTypeTasks(t))
            return false;

        if (IsFeatureUpgrade && ((TeamTaskFeatureUpgrade) this)?.NewProductFeature.Name == ((TeamTaskFeatureUpgrade) t)?.NewProductFeature.Name)
            return true;

        if (IsMarketingTask && ((TeamTaskChannelActivity) this).ChannelId == ((TeamTaskChannelActivity) t).ChannelId)
            return true;

        if (IsHighloadTask && ((TeamTaskSupportFeature) this).SupportFeature.Name == ((TeamTaskSupportFeature) t).SupportFeature.Name)
            return true;

        return false;
    }

    public bool AreSameTypeTasks(TeamTask t)
    {
        if (IsFeatureUpgrade && t.IsFeatureUpgrade)
            return true;

        if (IsMarketingTask && t.IsMarketingTask)
            return true;

        if (IsHighloadTask && t.IsHighloadTask)
            return true;

        return false;
    }

    public string GetTaskName()
    {
        if (IsFeatureUpgrade)
            return "Task: " + (this as TeamTaskFeatureUpgrade).NewProductFeature.Name;

        if (IsMarketingTask)
            return "Task: Channel" + (this as TeamTaskChannelActivity).ChannelId;

        if (IsHighloadTask)
            return "Task: Servers " + (this as TeamTaskSupportFeature).SupportFeature.Name;

        if (IsSupportTask)
            return "Task: " + (this as TeamTaskSupportFeature).SupportFeature.Name;

        return ToString();
    }

    public string GetPrettyName()
    {
        if (IsFeatureUpgrade)
            return "Feature: " + (this as TeamTaskFeatureUpgrade).NewProductFeature.Name;

        if (IsMarketingTask)
            return "Marketing in Channel" + (this as TeamTaskChannelActivity).ChannelId;

        if (IsHighloadTask)
            return (this as TeamTaskSupportFeature).SupportFeature.Name;

        if (IsSupportTask)
            return (this as TeamTaskSupportFeature).SupportFeature.Name;

        return ToString();
    }

    //public long GetCost()
    //{
    //    if (IsFeatureUpgrade)
    //        return 0;

    //    if (IsMarketingTask)
    //        return (this as TeamTaskChannelActivity).ChannelCost;

    //    if (IsSupportTask)
    //        return Teams.gettea (this as TeamTaskSupportFeature).SupportFeature.Name;
    //}

    //public override bool Equals(object obj)
    //{
    //    var t2 = obj as TeamTask;

    //    if (IsFeatureUpgrade && t2.IsFeatureUpgrade)
    //        return (this as TeamTaskFeatureUpgrade).NewProductFeature.Name == (t2 as TeamTaskFeatureUpgrade).NewProductFeature.Name;

    //    if (IsMarketingTask && t2.IsMarketingTask)
    //        return (this as TeamTaskChannelActivity).ChannelId == (t2 as TeamTaskChannelActivity).ChannelId;

    //    if (IsSupportTask && t2.IsSupportTask)
    //        return (this as TeamTaskSupportFeature).SupportFeature.Name == (t2 as TeamTaskSupportFeature).SupportFeature.Name;

    //    return false; // base.Equals(obj);
    //}

    //public override int GetHashCode()
    //{
    //    var hashCode = -262840398;
    //    hashCode = hashCode * -1521134295 + IsFeatureUpgrade.GetHashCode();
    //    hashCode = hashCode * -1521134295 + IsMarketingTask.GetHashCode();
    //    hashCode = hashCode * -1521134295 + IsSupportTask.GetHashCode();
    //    hashCode = hashCode * -1521134295 + IsHighloadTask.GetHashCode();
    //    return hashCode;
    //}
}

public class TeamTaskChannelActivity : TeamTask
{
    public int ChannelId;
    public long ChannelCost;

    public TeamTaskChannelActivity(int channelId, long cost)
    {
        this.ChannelId = channelId;
        this.ChannelCost = cost;
    }
    
    public  static TeamTaskChannelActivity FromChannel(ChannelInfo channelInfo) => new TeamTaskChannelActivity(channelInfo.ID, (long)channelInfo.costPerAd); 
}

public class TeamTaskFeatureUpgrade : TeamTask
{
    public NewProductFeature NewProductFeature;

    public TeamTaskFeatureUpgrade(NewProductFeature NewProductFeature)
    {
        this.NewProductFeature = NewProductFeature;
    }
}

public class TeamTaskSupportFeature : TeamTask
{
    public SupportFeature SupportFeature;

    public TeamTaskSupportFeature(SupportFeature supportFeature)
    {
        this.SupportFeature = supportFeature;
    }
}

//public class WorkingTeamInfo
//{
//    public TeamType TeamType;
//}

//[Game]
//public class WorkingTeamsComponent : IComponent
//{
//}

public class EmployeeComponent : IComponent
{
    public Dictionary<int, WorkerRole> Managers;
}

// Communication Effeciency

// Understand, what he does (prof area)
// Understand Human (Personal)

// after a while you create an effecient format of doing your job

[Game]
public class PersonalRelationshipsComponent : IComponent
{
    // int - humanId, float - progress
    public Dictionary<int, float> Relations;
}
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

[Game, Event(EventTarget.Any)]
public class NotificationsComponent : IComponent
{
    public List<NotificationMessage> Notifications;
}

public enum NotificationType
{
    LevelUp,
    InvestmentAccepted,
    InnovationOnNiche,
    NewCompanyOnNiche,
    Buying,
    Bankruptcy,

    InvestmentRoundStarted,
    CompanyBecomesProfitable,

    NicheTrends,
    BoughtShares,

    CompanyTypeChange,
    CompanyFocusChange,

    RecruitingManager
}

public class NotificationMessage
{
    public NotificationType NotificationType;
}

public class NotificationMessageManagerRecruiting : NotificationMessage
{
    public int HumanId;
    public int CompanyId;
    public bool Successful;

    public NotificationMessageManagerRecruiting(int humanId, int companyId, bool successful)
    {
        NotificationType = NotificationType.RecruitingManager;

        this.HumanId = humanId;
        this.CompanyId = companyId;
        Successful = successful;
    }
}

public class NotificationMessageBankruptcy : NotificationMessage
{
    public int CompanyId;

    public NotificationMessageBankruptcy(int CompanyId)
    {
        NotificationType = NotificationType.Bankruptcy;

        this.CompanyId = CompanyId;
    }
}

public class NotificationMessageInnovation : NotificationMessage
{
    public int CompanyId;
    public int Level;
    public bool Revolution;
    public long ClientGain;
    public int BrandGain;

    public NotificationMessageInnovation(int CompanyId, int level, bool revolution, long clientGain, int brandGain)
    {
        NotificationType = NotificationType.InnovationOnNiche;

        this.CompanyId = CompanyId;
        this.Level = level;

        this.Revolution = revolution;
        this.ClientGain = clientGain;
        this.BrandGain = brandGain;
    }
}

public class NotificationMessageNewCompany : NotificationMessage
{
    public int CompanyId;

    public NotificationMessageNewCompany(int CompanyId)
    {
        NotificationType = NotificationType.NewCompanyOnNiche;

        this.CompanyId = CompanyId;
    }
}

public class NotificationMessageTrendsChange : NotificationMessage
{
    public NicheType nicheType;

    public NotificationMessageTrendsChange(NicheType nicheType)
    {
        NotificationType = NotificationType.NicheTrends;

        this.nicheType = nicheType;
    }
}

public class NotificationMessageBuyingCompany : NotificationMessage
{
    public int CompanyId;
    public int BuyerInvestorId;
    public long Bid;

    public NotificationMessageBuyingCompany(int CompanyId, int BuyerInvestorId, long Bid)
    {
        NotificationType = NotificationType.Buying;

        this.CompanyId = CompanyId;
        this.Bid = Bid;
        this.BuyerInvestorId = BuyerInvestorId;
    }
}

public class NotificationMessageCompanyFocusChange : NotificationMessage
{
    public int CompanyId;
    // true - added, false - removed
    public bool Added;
    public NicheType NicheType;

    public NotificationMessageCompanyFocusChange(int CompanyId, bool Added, NicheType nicheType)
    {
        NotificationType = NotificationType.CompanyFocusChange;

        this.CompanyId = CompanyId;
        this.Added = Added;
        this.NicheType = nicheType;
    }
}

public class NotificationMessageBuyingShares : NotificationMessage
{
    public int CompanyId;
    public int BuyerInvestorId;

    public float BlockOfSharesSize;
    public long Bid;

    public NotificationMessageBuyingShares(int CompanyId, int BuyerInvestorId, long Bid)
    {
        NotificationType = NotificationType.BoughtShares;

        this.CompanyId = CompanyId;
        this.Bid = Bid;
        this.BuyerInvestorId = BuyerInvestorId;
    }
}

//public class NotificationMessageLevelUp : NotificationMessage
//{
//    public int CompanyId;
//    public int Level;

//    public NotificationMessageLevelUp(int CompanyId, int Level)
//    {
//        NotificationType = NotificationType.LevelUp;

//        this.CompanyId = CompanyId;
//        this.Level = Level;
//    }
//}

public class NotificationMessageInvestmentRoundStarted : NotificationMessage
{
    public int CompanyId;

    public NotificationMessageInvestmentRoundStarted(int CompanyId)
    {
        NotificationType = NotificationType.InvestmentRoundStarted;

        this.CompanyId = CompanyId;
    }
}

public class NotificationMessageCompanyTypeChange : NotificationMessage
{
    public int CompanyId;
    public string PreviousName;

    public NotificationMessageCompanyTypeChange(int CompanyId, string PreviousName)
    {
        NotificationType = NotificationType.CompanyTypeChange;

        this.CompanyId = CompanyId;
        this.PreviousName = PreviousName;
    }
}
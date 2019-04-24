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
    CompanyBecomesProfitable
}

public class NotificationMessage
{
    public NotificationType NotificationType;
}

public class NotificationBankruptcy : NotificationMessage
{
    public int CompanyId;

    public NotificationBankruptcy(int CompanyId)
    {
        NotificationType = NotificationType.Bankruptcy;

        this.CompanyId = CompanyId;
    }
}

public class NotificationBuyingCompany : NotificationMessage
{
    public int CompanyId;
    public int BuyerInvestorId;
    public long Bid;

    public NotificationBuyingCompany(int CompanyId, int BuyerInvestorId, long Bid)
    {
        NotificationType = NotificationType.Buying;

        this.CompanyId = CompanyId;
        this.Bid = Bid;
        this.BuyerInvestorId = BuyerInvestorId;
    }
}

public class NotificationLevelUp : NotificationMessage
{
    public int CompanyId;
    public int Level;

    public NotificationLevelUp(int CompanyId, int Level)
    {
        NotificationType = NotificationType.LevelUp;

        this.CompanyId = CompanyId;
        this.Level = Level;
    }
}

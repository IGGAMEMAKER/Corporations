using Entitas;
using System.Collections.Generic;

[Game]
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

}

[Game]
public struct NotificationMessage
{
    public NotificationType NotificationType;
    
}

using Entitas;
using System.Collections.Generic;


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

    public PopupMessageCompanyClose(int companyId)
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
    public PopupMessageCompetitorWantsToInterceptOurTargetCompany(int targetCompanyId, int interceptorId) : base(targetCompanyId, PopupType.TargetInterception)
    {
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
    public PopupMessageMarketPhaseChange(NicheType niche) : base(niche, PopupType.MarketChanges) { }
}
public class PopupMessageMarketInspiration : PopupMessageMarketChanges
{
    public PopupMessageMarketInspiration(NicheType niche) : base(niche, PopupType.InspirationToOpenMarket) { }
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

using Entitas;
using System.Collections.Generic;


public enum PopupType
{
    CloseCompany,
    MarketChanges,

    BankruptCompany,
    NewCompany,
    TargetWasBought,
    NewCorporation,

    InspirationToOpenMarket,
    InterestToCompanyInOurSphereOfInfluence
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

public class PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence : PopupMessageCompanyEvent
{
    public int InterceptorCompanyId;
    public long Bid;
    public PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence(int targetCompanyId, int interceptorId, long bid) : base(targetCompanyId, PopupType.TargetWasBought)
    {
        InterceptorCompanyId = interceptorId;
        Bid = bid;
    }
}

public class PopupMessageInterestToCompany : PopupMessageCompanyEvent
{
    public int buyerInvestorId;
    public PopupMessageInterestToCompany(int companyId, int buyerInvestorId) : base(companyId, PopupType.InterestToCompanyInOurSphereOfInfluence) {
        this.buyerInvestorId = buyerInvestorId;
    }
}

public class PopupMessageCorporationSpawn : PopupMessageCompanyEvent
{
    public PopupMessageCorporationSpawn(int companyId) : base(companyId, PopupType.NewCorporation) { }
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

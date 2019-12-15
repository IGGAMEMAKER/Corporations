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
    InterestToCompanyInOurSphereOfInfluence,

    StrategicPartnership,

    PreRelease,
    Release,

    CreatePrototype,
    CreatePrototypeWarning,

    TooManyPartners,

    BankruptcyThreat,
    GameOver,

    CorporationRequirementsWarning
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

public class PopupMessageStrategicPartnership : PopupMessage
{
    public int companyId;
    public int companyId2;

    // form = true, break = false
    public PopupMessageStrategicPartnership(int companyId, int companyId2, bool form)
    {
        this.companyId = companyId;
        this.companyId2 = companyId2;

        PopupType = PopupType.StrategicPartnership;
    }
}

public class PopupMessageTooManyPartners : PopupMessageCompanyEvent
{
    public PopupMessageTooManyPartners(int companyId) : base(companyId, PopupType.TooManyPartners) { }
}

public class PopupMessageCompanyBankrupt : PopupMessageCompanyEvent
{
    public PopupMessageCompanyBankrupt(int companyId) : base(companyId, PopupType.BankruptCompany) { }
}

public class PopupMessageDoYouWantToRelease : PopupMessageCompanyEvent
{
    public PopupMessageDoYouWantToRelease(int companyId) : base(companyId, PopupType.PreRelease) { }
}

public class PopupMessageCreateApp : PopupMessageCompanyEvent
{
    public PopupMessageCreateApp(int companyId) : base(companyId, PopupType.CreatePrototype) { }
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

public class PopupMessageBankruptcyThreat : PopupMessageCompanyEvent
{
    public PopupMessageBankruptcyThreat(int companyId) : base(companyId, PopupType.BankruptcyThreat) { }
}

public class PopupMessageGameOver : PopupMessageCompanyEvent
{
    public PopupMessageGameOver(int companyId) : base(companyId, PopupType.GameOver) { }
}

public class PopupMessageCompanySpawn : PopupMessageCompanyEvent
{
    public PopupMessageCompanySpawn(int companyId) : base(companyId, PopupType.NewCompany) { }
}

public class PopupMessageCorporationRequirements : PopupMessageCompanyEvent
{
    public PopupMessageCorporationRequirements(int companyId) : base(companyId, PopupType.CorporationRequirementsWarning) { }
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

public class PopupMessageDoYouWantToCreateApp : PopupMessageMarketChanges
{
    public PopupMessageDoYouWantToCreateApp(NicheType niche) : base(niche, PopupType.CreatePrototypeWarning) { }
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

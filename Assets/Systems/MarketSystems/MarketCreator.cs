//public class MarketCreator

public partial struct MarketProfile
{
    public AudienceSize AudienceSize;
    public Monetisation MonetisationType;
    public Margin Margin;


    public NicheSpeed NicheSpeed;
    public AppComplexity AppComplexity;

    public MarketProfile(AudienceSize AudienceSize, Monetisation MonetisationType, Margin Margin, AppComplexity AppComplexity, NicheSpeed NicheSpeed)
    {
        this.AudienceSize = AudienceSize;
        this.MonetisationType = MonetisationType;
        this.Margin = Margin;
        this.AppComplexity = AppComplexity;
        this.NicheSpeed = NicheSpeed;
    }

    // audiences
    public MarketProfile SetAudience(AudienceSize audienceSize)
    {
        AudienceSize = audienceSize;
        return this;
    }
    public MarketProfile ForEnterpriseSmall() => SetAudience(AudienceSize.SmallEnterprise);
    public MarketProfile ForEnterpriseBig() => SetAudience(AudienceSize.BigEnterprise);
    public MarketProfile NicheApp() => SetAudience(AudienceSize.Million);
    public MarketProfile Popular() => SetAudience(AudienceSize.Million100);
    public MarketProfile Global() => SetAudience(AudienceSize.Global);

    // monetisation
    public MarketProfile SetMonetisation(Monetisation monetisation)
    {
        MonetisationType = monetisation;
        return this;
    }
    public MarketProfile Free() => SetMonetisation(Monetisation.Adverts);
    public MarketProfile Service() => SetMonetisation(Monetisation.Service);


    // complexity
    public MarketProfile SetComplexity(AppComplexity appComplexity)
    {
        AppComplexity = appComplexity;
        return this;
    }
    public MarketProfile SoloDev() => SetComplexity(AppComplexity.Solo);
    public MarketProfile Util() => SetComplexity(AppComplexity.Solo);
    public MarketProfile Humongous() => SetComplexity(AppComplexity.Humongous);

    public MarketProfile BigWebService() => SetComplexity(AppComplexity.Hard);
    public MarketProfile WebService() => SetComplexity(AppComplexity.Average);
    public MarketProfile SmallWebService() => SetComplexity(AppComplexity.Easy);


    // margin
    public MarketProfile SetMargin(Margin margin)
    {
        Margin = margin;
        return this;
    }
    public MarketProfile IncomeLow() => SetMargin(Margin.Low);
    public MarketProfile IncomeMid() => SetMargin(Margin.Mid);
    public MarketProfile IncomeHigh() => SetMargin(Margin.High);

    // market changes speed
    public MarketProfile SetSpeed(NicheSpeed nicheSpeed)
    {
        NicheSpeed = nicheSpeed;
        return this;
    }
    public MarketProfile Dynamic() => SetSpeed(NicheSpeed.Quarter);
    public MarketProfile LongTerm() => SetSpeed(NicheSpeed.ThreeYears);
}

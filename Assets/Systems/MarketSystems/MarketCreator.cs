//public class MarketCreator

public partial struct MarketProfile
{
    public AudienceSize AudienceSize;
    public Monetisation MonetisationType;
    public Margin Margin;


    public NicheSpeed NicheSpeed;
    public AppComplexity AppComplexity;


    // audiences
    public MarketProfile Popular()
    {
        AudienceSize = AudienceSize.Million100;
        return this;
    }

    public MarketProfile Global()
    {
        AudienceSize = AudienceSize.Global;
        return this;
    }

    // monetisation
    public MarketProfile Free()
    {
        MonetisationType = Monetisation.Adverts;
        return this;
    }

    public MarketProfile Service()
    {
        MonetisationType = Monetisation.Service;
        return this;
    }

    // complexity
    public MarketProfile SoloDev()
    {
        AppComplexity = AppComplexity.Solo;
        return this;
    }
    public MarketProfile Humongous()
    {
        AppComplexity = AppComplexity.Humongous;
        return this;
    }
    public MarketProfile BigWebService()
    {
        AppComplexity = AppComplexity.Hard;
        return this;
    }

    // margin
    public MarketProfile SetMargin(Margin margin)
    {
        Margin = margin;
        return this;
    }

    // market changes speed
    public MarketProfile SetNicheSpeed(NicheSpeed nicheSpeed)
    {
        NicheSpeed = nicheSpeed;
        return this;
    }
}

using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NicheTableView : View, IPointerEnterHandler
{
    public Text NicheName;
    public Text Competitors;
    public Image Panel;
    public Text StartCapital;
    public Hint Phase;
    public Text NicheSize;
    public SetAmountOfStars SetAmountOfStars;
    public Text MarketPhase;

    public Text MonetisationType;

    GameEntity niche;

    public void SetEntity(GameEntity niche)
    {
        this.niche = niche;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        if (niche == null)
            return;

        SetPanelColor();

        var nicheType = niche.niche.NicheType;

        GetComponent<LinkToNiche>().SetNiche(nicheType);

        NicheName.text = EnumUtils.GetFormattedNicheName(nicheType);

        var hasCompany = CompanyUtils.HasCompanyOnMarket(MyCompany, nicheType, GameContext);
        var isInterestingMarket = MyCompany.companyFocus.Niches.Contains(nicheType);
        var colorName = hasCompany || isInterestingMarket ?
            VisualConstants.COLOR_MARKET_ATTITUDE_HAS_COMPANY
            :
            VisualConstants.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;
        NicheName.color = Visuals.GetColorFromString(colorName);


        DescribePhase();
        //var growth = NicheUtils.GetAbsoluteAnnualMarketGrowth(GameContext, entity);

        var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
        MonetisationType.text = GetFormattedMonetisationType(monetisation);

        RenderTimeToMarket();
        
        // maintenance
        var capital = NicheUtils.GetStartCapital(niche);
        StartCapital.text = Format.MinifyMoney(capital);

        var myBalance = EconomyUtils.GetCompanyBalance(MyCompany);
        StartCapital.color = Visuals.GetColorPositiveOrNegative(myBalance - capital);



        var profitLeader = NicheUtils.GetMostProfitableCompanyOnMarket(GameContext, niche);
        var profit = profitLeader == null ? 0 : EconomyUtils.GetProfit(profitLeader, GameContext);
        
        NicheSize.text = Format.MinifyMoney(profit);
        NicheSize.color = Visuals.GetColorPositiveOrNegative(profit);
    }

    void RenderTimeToMarket()
    {
        // time to market
        var demand = ProductUtils.GetMarketDemand(niche);
        var iterationTime = ProductUtils.GetBaseIterationTime(niche);
        var timeToMarket = demand * iterationTime / 30;

        Competitors.text = $"{timeToMarket} months";
    }

    string GetFormattedMonetisationType (Monetisation monetisation)
    {
        switch (monetisation)
        {
            case Monetisation.IrregularPaid: return "Irregular paid";
            default: return monetisation.ToString();
        }
    }

    void DescribePhase()
    {
        var phase = NicheUtils.GetMarketState(niche);
        Phase.SetHint(phase.ToString());

        // phase
        var stars = NicheUtils.GetMarketRating(niche);

        SetAmountOfStars.SetStars(stars);
        MarketPhase.text = phase.ToString();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedNiche(GameContext, niche.niche.NicheType);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(niche.niche.NicheType == ScreenUtils.GetSelectedNiche(GameContext));
    }
}

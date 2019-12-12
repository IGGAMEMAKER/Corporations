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

        RenderMarketName(nicheType);


        DescribePhase();
        //var growth = NicheUtils.GetAbsoluteAnnualMarketGrowth(GameContext, entity);

        var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
        MonetisationType.text = GetFormattedMonetisationType(monetisation);

        RenderTimeToMarket();
        
        // 
        var profitLeader = Markets.GetMostProfitableCompanyOnMarket(GameContext, niche);
        
        // maintenance
        var biggestMaintenance = profitLeader == null ? 0 : EconomyUtils.GetCompanyMaintenance(profitLeader, GameContext);
        StartCapital.text = Format.MinifyMoney(biggestMaintenance);

        var myProfit = EconomyUtils.GetProfit(MyCompany, GameContext);
        StartCapital.color = Visuals.GetColorPositiveOrNegative(myProfit - biggestMaintenance);

        // income
        var profit = profitLeader == null ? 0 : EconomyUtils.GetProfit(profitLeader, GameContext);

        var ROI = profitLeader == null ? 0 : (profit * 100 / biggestMaintenance);
        
        NicheSize.text = profitLeader == null ? "???" : ROI + "%";
        NicheSize.color = Visuals.GetColorPositiveOrNegative(profit);

    }

    void RenderMarketName(NicheType nicheType)
    {
        var industryType = Markets.GetIndustry(nicheType, GameContext);
        NicheName.text = EnumUtils.GetFormattedNicheName(nicheType) + "\n<i>" + EnumUtils.GetFormattedIndustryName(industryType) + "</i>";

        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, nicheType, GameContext);
        var isInterestingMarket = MyCompany.companyFocus.Niches.Contains(nicheType);
        var colorName = hasCompany || isInterestingMarket ?
            VisualConstants.COLOR_MARKET_ATTITUDE_HAS_COMPANY
            :
            VisualConstants.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;
        NicheName.color = Visuals.GetColorFromString(colorName);
    }

    void RenderTimeToMarket()
    {
        // time to market
        var demand = Products.GetMarketDemand(niche);
        var iterationTime = Products.GetBaseIterationTime(niche);
        var timeToMarket = demand * iterationTime / 30;

        Competitors.text = $"{timeToMarket}\nmonths";
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
        var phase = Markets.GetMarketState(niche);
        Phase.SetHint(phase.ToString());

        // phase
        var stars = Markets.GetMarketRating(niche);

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

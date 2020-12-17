using Assets.Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NicheTableView : View, IPointerEnterHandler
{
    public Text NicheName;
    public Text Competitors;
    public Image Panel;
    public Text Maintenance;
    public Hint Phase;
    public Text NicheSize;
    public SetAmountOfStars SetAmountOfStars;
    public Text MarketPhase;

    public Text MonetisationType;
    public Text StartCapital;

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

        GetComponent<LinkToNicheInfo>().SetNiche(nicheType);

        RenderMarketName(nicheType);


        DescribePhase();
        //var growth = NicheUtils.GetAbsoluteAnnualMarketGrowth(GameContext, entity);

        MonetisationType.text = Enums.GetFormattedMonetisationType(niche);

        RenderTimeToMarket();

        // 
        var profitLeader = Markets.GetMostProfitableCompanyOnMarket(Q, niche);

        var profit              = profitLeader == null ? 0 : Economy.GetProfit(Q, profitLeader);
        var income              = profitLeader == null ? 0 : Economy.GetIncome(Q, profitLeader);
        var biggestMaintenance  = profitLeader == null ? 0 : Economy.GetMaintenance(Q, profitLeader);

        // maintenance
        Maintenance.text = Visuals.Positive(Format.MinifyMoney(income));

        bool canMaintain = Economy.IsCanMaintain(MyCompany, Q, biggestMaintenance);
        StartCapital.text = Visuals.Colorize(Format.MinifyMoney(biggestMaintenance), canMaintain);
        // income
        var ROI = Markets.GetMarketROI(Q, niche);
        
        NicheSize.text = profitLeader == null ? "???" : ROI + "%";
        NicheSize.color = Visuals.GetColorPositiveOrNegative(profit);
    }

    void RenderMarketName(NicheType nicheType)
    {
        var industryType = Markets.GetIndustry(nicheType, Q);


        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, nicheType, Q);
        var isInterestingMarket = MyCompany.companyFocus.Niches.Contains(nicheType);

        var marketColorName = Colors.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;

        if (hasCompany)
            marketColorName = Colors.COLOR_MARKET_ATTITUDE_HAS_COMPANY;
        else if (isInterestingMarket)
            marketColorName = Colors.COLOR_MARKET_ATTITUDE_FOCUS_ONLY;

        
        bool isOurIndustry = MyCompany.companyFocus.Industries.Contains(industryType);
        var industryColorName = isOurIndustry ? Colors.COLOR_MARKET_ATTITUDE_HAS_COMPANY : Colors.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;


        var coloredMarket = Visuals.Colorize(Enums.GetFormattedNicheName(nicheType), marketColorName);
        var coloredIndustry = Visuals.Colorize(Enums.GetFormattedIndustryName(industryType), industryColorName);

        NicheName.text = $"{coloredMarket}\n<i>{coloredIndustry}</i>";
    }

    void RenderTimeToMarket()
    {
        // time to market
        var timeToMarket = Products.GetTimeToMarketFromScratch(niche);

        Competitors.text = $"{timeToMarket}\nmonths";
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
        ScreenUtils.SetSelectedNiche(Q, niche.niche.NicheType);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(niche.niche.NicheType == SelectedNiche);
    }
}

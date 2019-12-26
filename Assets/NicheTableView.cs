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

        var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;
        MonetisationType.text = GetFormattedMonetisationType(monetisation);

        RenderTimeToMarket();

        var sumOfBrandPowers = (int)MarketingUtils.GetSumOfBrandPowers(niche, GameContext);

        // 
        var profitLeader = Markets.GetMostProfitableCompanyOnMarket(GameContext, niche);

        var profit              = profitLeader == null ? 0 : Economy.GetProfit(profitLeader, GameContext);
        var income              = profitLeader == null ? 0 : Economy.GetCompanyIncome(profitLeader, GameContext);
        var biggestMaintenance  = profitLeader == null ? 0 : Economy.GetCompanyMaintenance(GameContext, profitLeader);

        // maintenance
        Maintenance.text = Visuals.Positive(Format.MinifyMoney(income));

        bool canMaintain = Economy.IsCanMaintain(MyCompany, GameContext, biggestMaintenance);
        StartCapital.text = Visuals.Colorize(Format.MinifyMoney(biggestMaintenance), canMaintain);
        // income
        var ROI = Markets.GetMarketROI(GameContext, niche);
        
        NicheSize.text = profitLeader == null ? "???" : ROI + "%";
        NicheSize.color = Visuals.GetColorPositiveOrNegative(profit);
    }

    void RenderMarketName(NicheType nicheType)
    {
        var industryType = Markets.GetIndustry(nicheType, GameContext);


        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, nicheType, GameContext);
        var isInterestingMarket = MyCompany.companyFocus.Niches.Contains(nicheType);

        var marketColorName = VisualConstants.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;

        if (hasCompany)
            marketColorName = VisualConstants.COLOR_MARKET_ATTITUDE_HAS_COMPANY;
        else if (isInterestingMarket)
            marketColorName = VisualConstants.COLOR_MARKET_ATTITUDE_FOCUS_ONLY;

        
        bool isOurIndustry = MyCompany.companyFocus.Industries.Contains(industryType);
        var industryColorName = isOurIndustry ? VisualConstants.COLOR_MARKET_ATTITUDE_HAS_COMPANY : VisualConstants.COLOR_MARKET_ATTITUDE_NOT_INTERESTED;


        var coloredMarket = Visuals.Colorize(EnumUtils.GetFormattedNicheName(nicheType), marketColorName);
        var coloredIndustry = Visuals.Colorize(EnumUtils.GetFormattedIndustryName(industryType), industryColorName);

        NicheName.text = $"{coloredMarket}\n<i>{coloredIndustry}</i>";
    }

    void RenderTimeToMarket()
    {
        // time to market
        var timeToMarket = Products.GetTimeToMarketFromScratch(niche);

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

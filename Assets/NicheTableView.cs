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

    GameEntity entity;

    public void SetEntity(GameEntity niche)
    {
        entity = niche;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        if (entity == null)
            return;

        SetPanelColor();

        var nicheType = entity.niche.NicheType;

        GetComponent<LinkToNiche>().SetNiche(nicheType);

        NicheName.text = EnumUtils.GetFormattedNicheName(nicheType);
        Competitors.text = NicheUtils.GetCompetitorsAmount(nicheType, GameContext) + "\ncompanies";

        var monetisation = entity.nicheBaseProfile.Profile.MonetisationType;
        MonetisationType.text = GetFormattedMonetisationType(monetisation);

        DescribePhase();
        NicheSize.text = Format.Money(NicheUtils.GetBiggestIncomeOnMarket(GameContext, entity));
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
        var phase = NicheUtils.GetMarketState(entity);
        Phase.SetHint(phase.ToString());

        //var growth = NicheUtils.GetAbsoluteAnnualMarketGrowth(GameContext, entity);
        var capital = NicheUtils.GetStartCapital(entity);
        StartCapital.text = Format.Money(capital);

        var stars = NicheUtils.GetMarketRating(entity);

        SetAmountOfStars.SetStars(stars);
        MarketPhase.text = phase.ToString();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedNiche(GameContext, entity.niche.NicheType);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity.niche.NicheType == ScreenUtils.GetSelectedNiche(GameContext));
    }
}

using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NicheTableView : View, IPointerEnterHandler
{
    [SerializeField] Text NicheName;
    [SerializeField] Text Competitors;
    [SerializeField] Image Panel;
    [SerializeField] Text Growth;
    [SerializeField] Text Phase;
    [SerializeField] Text NicheSize;
    

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
        SetPanelColor();

        NicheName.text = EnumUtils.GetFormattedNicheName(entity.niche.NicheType);
        Competitors.text = NicheUtils.GetCompetitorsAmount(entity.niche.NicheType, GameContext) + "\ncompanies";

        var phase = entity.nicheState.Phase;
        Phase.text = phase.ToString();
        Growth.text = $"{Format.Sign(entity.nicheState.Growth[phase])}%\ngrowth";

        NicheSize.text = Format.MinifyToInteger(GetMarketPotential(entity));
    }

    //public int GetAveragePhasePeriodForMarket(NicheLifecyclePhase phase)
    //{
    //    switch (phase)
    //    {
    //        case NicheLifecyclePhase.Innovation:
    //    }
    //}

    public static long GetMarketPotential(GameEntity niche)
    {
        var state = niche.nicheState;

        var clientBatch = niche.nicheCosts.ClientBatch;
        var price = niche.nicheCosts.BasePrice * 1.5f;

        long clients = 0;

        foreach (var g in state.Growth)
        {
            var phasePeriod = NicheUtils.GetMinimumPhaseDurationModifier(g.Key) * 30;

            var brandModifier = 1.5f;
            var financeReach = MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High);

            clients += (long)(clientBatch * g.Value * phasePeriod * brandModifier * financeReach);
        }

        print($"Clients expectation for {niche.niche.NicheType}: " + clients);

        return (long)(clients * CompanyEconomyUtils.GetCompanyCostNicheMultiplier() * price);
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

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
        if (entity == null)
            return;

        SetPanelColor();

        NicheName.text = EnumUtils.GetFormattedNicheName(entity.niche.NicheType);
        Competitors.text = NicheUtils.GetCompetitorsAmount(entity.niche.NicheType, GameContext) + "\ncompanies";

        var phase = entity.nicheState.Phase;
        Phase.text = phase.ToString();
        Growth.text = $"{Format.Sign(entity.nicheState.Growth[phase])}%\ngrowth";

        NicheSize.text = Format.MinifyToInteger(NicheUtils.GetMarketPotential(entity));
    }

    //public int GetAveragePhasePeriodForMarket(NicheLifecyclePhase phase)
    //{
    //    switch (phase)
    //    {
    //        case NicheLifecyclePhase.Innovation:
    //    }
    //}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ScreenUtils.SetSelectedNiche(GameContext, entity.niche.NicheType);
    }

    void SetPanelColor()
    {
        Panel.color = GetPanelColor(entity.niche.NicheType == ScreenUtils.GetSelectedNiche(GameContext));
    }
}

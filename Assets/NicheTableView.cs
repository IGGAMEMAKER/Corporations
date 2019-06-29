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
    [SerializeField] SetAmountOfStars SetAmountOfStars;
    

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


        DescribePhase();
        NicheSize.text = Format.MinifyToInteger(NicheUtils.GetMarketPotential(entity));
    }

    void DescribePhase()
    {
        var phase = entity.nicheState.Phase;
        Phase.text = phase.ToString();
        Growth.text = $"{Format.Sign(entity.nicheState.Growth[phase])}%";

        var stars = 0;

        switch (phase)
        {
            case NicheLifecyclePhase.Idle: stars = 1; break;
            case NicheLifecyclePhase.Innovation: stars = 2; break;
            case NicheLifecyclePhase.Trending: stars = 4; break;
            case NicheLifecyclePhase.MassUse: stars = 5; break;
            case NicheLifecyclePhase.Decay: stars = 3; break;
        }

        SetAmountOfStars.SetStars(stars);
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

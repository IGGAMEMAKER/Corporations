using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NicheTableView : View, IPointerEnterHandler
{
    [SerializeField] Text NicheName;
    [SerializeField] Text Competitors;
    [SerializeField] Image Panel;
    [SerializeField] Text Growth;
    [SerializeField] Hint Phase;
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
        Phase.SetHint(phase.ToString());

        var growth = entity.nicheState.Growth[phase] * 12;
        Growth.text = $"{Format.Sign(growth)}%";

        var stars = NicheUtils.GetMarketRating(entity);

        SetAmountOfStars.SetStars(stars);
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

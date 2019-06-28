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
        Growth.text = $"{phase} \n{Format.Sign(entity.nicheState.Growth[phase])}%\ngrowth";
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

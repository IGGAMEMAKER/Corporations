using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class NicheScreenView : View
{
    public Text NicheName;
    public Text IndustryName;
    public Text AmountOfCompetitors;

    void RenderAmountOfCompanies(NicheType NicheType)
    {
        AmountOfCompetitors.text = "(" + NicheUtils.GetCompetitorsAmount(NicheType, GameContext) + ")";
    }

    void Render()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = EnumUtils.GetFormattedNicheName(NicheType);
        IndustryName.text = Visuals.Link("Is part of " + EnumUtils.GetFormattedIndustryName(IndustryType) + " industry");
        //IndustryName.gameObject.SetActive(false);

        RenderAmountOfCompanies(NicheType);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}

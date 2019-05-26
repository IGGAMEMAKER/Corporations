using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class NicheScreenView : View
    , IAnyCompanyListener
{
    public Text NicheName;
    public Text IndustryName;
    public MarketPotentialView MarketPotentialView;
    public Text AmountOfCompetitors;

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        if (entity.hasProduct)
            RenderAmountOfCompanies(entity.product.Niche);
    }

    void OnEnable()
    {
        Render();

        GetUniversalListener.AddAnyCompanyListener(this);
    }

    void RenderAmountOfCompanies(NicheType NicheType)
    {
        AmountOfCompetitors.text = "(" + NicheUtils.GetCompetitorsAmount(NicheType, GameContext) + ")";
    }

    void Render()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        NicheName.text = EnumUtils.GetFormattedNicheName(NicheType);
        IndustryName.text = VisualUtils.Link("Is part of " + EnumUtils.GetFormattedIndustryName(IndustryType) + " industry");

        MarketPotentialView.Render(NicheType);
        RenderAmountOfCompanies(NicheType);
    }
}

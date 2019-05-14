using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class NicheScreenView : View
    , IAnyCompanyListener
{
    public Text NicheName;
    public Text IndustryName;
    public LinkTo IndustryLink;
    public MarketPotentialView MarketPotentialView;
    public Text AmountOfCompetitors;

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        RenderAmountOfCompanies();
    }

    void OnEnable()
    {
        Render();

        GetUniversalListener.AddAnyCompanyListener(this);
    }

    void RenderAmountOfCompanies()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);

        AmountOfCompetitors.text = "(" + NicheUtils.GetCompetitorsAmount(NicheType, GameContext) + ")";
    }

    void Render()
    {
        NicheType NicheType = ScreenUtils.GetSelectedNiche(GameContext);
        IndustryType IndustryType = NicheUtils.GetIndustry(NicheType, GameContext);

        // "Niche: " + 
        NicheName.text = EnumUtils.GetFormattedNicheName(NicheType);
        IndustryName.text = VisualUtils.Link("Is part of " + EnumUtils.GetFormattedIndustryName(IndustryType) + " industry");
        //IndustryLink.

        MarketPotentialView.SetEntity(NicheType);
        RenderAmountOfCompanies();
    }
}

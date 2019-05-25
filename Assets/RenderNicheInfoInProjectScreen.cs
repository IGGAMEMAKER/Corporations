using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View
{
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    public StealIdeasController StealIdeasController;

    public override void ViewRender()
    {
        base.ViewRender();

        ToggleNicheObjects(CanRenderNiche);

        if (CanRenderNiche)
            RenderNicheTab();
    }

    bool CanRenderNiche
    {
        get
        {
            return SelectedCompany.company.CompanyType == CompanyType.ProductCompany;
        }
    }

    void ToggleNicheObjects(bool show)
    {
        NicheRoot.SetActive(show);

        StealIdeasController.gameObject.SetActive(show && IsMyCompetitor);
    }

    private void RenderNicheTab()
    {
        NicheType niche = SelectedCompany.product.Niche;

        string text = EnumUtils.GetFormattedNicheName(niche);

        NicheName.text = VisualUtils.Link(text);

        LinkToNiche.SetNiche(niche);
    }
}

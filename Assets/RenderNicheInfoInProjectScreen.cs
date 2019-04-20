using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View
    //, IMenuListener
{
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    void Start()
    {
        //ListenMenuChanges(this);

        Render();
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        CompanyType companyType = SelectedCompany.company.CompanyType;

        bool canRenderNiche = companyType == CompanyType.ProductCompany;

        ToggleNicheObjects(canRenderNiche);

        if (canRenderNiche)
            RenderNicheTab();
    }

    void ToggleNicheObjects(bool show)
    {
        NicheRoot.SetActive(show);
    }

    private void RenderNicheTab()
    {
        NicheType niche = SelectedCompany.product.Niche;

        string text = EnumUtils.GetFormattedNicheName(niche);

        NicheName.text = VisualFormattingUtils.Link(text);

        LinkToNiche.SetNiche(niche);
    }
}

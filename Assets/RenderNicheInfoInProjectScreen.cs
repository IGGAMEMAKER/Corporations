using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View
    //, IMenuListener
{
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    public StealIdeasController StealIdeasController;

    void Start()
    {
        //ListenMenuChanges(this);

        Render();
    }

    void OnEnable()
    {
        Render();
    }

    bool canRenderNiche
    {
        get
        {
            return SelectedCompany.company.CompanyType == CompanyType.ProductCompany;
        }
    }

    void Render()
    {
        ToggleNicheObjects(canRenderNiche);

        if (canRenderNiche)
            RenderNicheTab();
    }

    void ToggleNicheObjects(bool show)
    {
        NicheRoot.SetActive(show);

        if (StealIdeasController != null)
            StealIdeasController.gameObject.SetActive(show);
    }

    private void RenderNicheTab()
    {
        NicheType niche = SelectedCompany.product.Niche;

        string text = EnumUtils.GetFormattedNicheName(niche);

        NicheName.text = VisualFormattingUtils.Link(text);

        LinkToNiche.SetNiche(niche);
    }
}

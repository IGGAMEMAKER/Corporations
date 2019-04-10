using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class RenderNicheInfoInProjectScreen : View, IMenuListener
{
    public Text NicheName;
    public GameObject NicheRoot;
    public LinkToNiche LinkToNiche;

    void Start()
    {
        ListenMenuChanges(this);

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

        string text = EnumFormattingUtils.GetFormattedNicheName(niche);

        NicheName.text = VisualFormattingUtils.Link(text);

        LinkToNiche.SetNiche(niche);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}

public static class VisualFormattingUtils
{
    public static string Link(string text)
    {
        return $"<i><b><color=blue>{text}</color></b></i>";
    }

    public static Color Color(string color)
    {
        ColorUtility.TryParseHtmlString(color, out Color c);

        return c;
    }
}
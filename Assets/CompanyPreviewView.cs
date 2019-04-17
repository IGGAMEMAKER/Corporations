using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

public class CompanyPreviewView : View,
    IProductListener
{
    public Text CompanyNameLabel;
    public Text CompanyTypeLabel;
    public Image Panel;
    public Text CEOLabel;

    public Text ShareCostLabel;

    public GameEntity _entity;

    Color baseColor;

    void Awake()
    {
        baseColor = Panel.color;
    }

    public void SetEntity(GameEntity entity)
    {
        _entity = entity;

        entity.AddProductListener(this);

        ColorUtility.TryParseHtmlString(VisualConstants.COLOR_COMPANY_SELECTED, out Color selectedCompanyColor);

        if (entity == SelectedCompany && MenuUtils.GetMenu(GameContext).menu.ScreenMode == ScreenMode.GroupManagementScreen)
            Panel.color = selectedCompanyColor;
        else
            Panel.color = baseColor;

        CEOLabel.gameObject.SetActive(entity.isControlledByPlayer);

        Render(entity);
    }

    void RenderCompanyType(GameEntity entity)
    {
        CompanyTypeLabel.text = EnumFormattingUtils.GetFormattedCompanyType(entity.company.CompanyType);
    }

    void RenderCompanyName(GameEntity entity)
    {
        CompanyNameLabel.text = entity.company.Name;
    }

    void UpdateLinkToCompany(GameEntity e)
    {
        var link = GetComponent<LinkToCompanyPreview>();

        if (link != null)
            link.CompanyId = e.company.Id;
    }

    void Render(GameEntity e)
    {
        RenderCompanyName(e);
        RenderCompanyType(e);

        RenderCompanyCost(e);

        UpdateLinkToCompany(e);
    }

    private void RenderCompanyCost(GameEntity e)
    {
        ShareCostLabel.text = "$" + ValueFormatter.Shorten(CompanyUtils.GetCompanyCost(GameContext, e.company.Id));
    }

    public void OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel)
    {
        Render(entity);
    }
}
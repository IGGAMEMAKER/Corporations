using Assets.Classes;
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

    public void SetEntity(GameEntity entity)
    {
        _entity = entity;

        entity.AddProductListener(this);

        ColorUtility.TryParseHtmlString(VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO, out Color ourCompanyColor);

        //if (entity.isControlledByPlayer)
        //{
        //    Panel.color = ourCompanyColor;
        //}

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

    void Render(GameEntity e)
    {
        RenderCompanyName(e);
        RenderCompanyType(e);

        GetComponent<LinkToCompanyPreview>().CompanyId = e.company.Id;
    }

    public void OnProduct(GameEntity entity, int id, string name, NicheType niche, IndustryType industry, int productLevel, int explorationLevel, TeamResource resources)
    {
        Render(entity);
    }
}
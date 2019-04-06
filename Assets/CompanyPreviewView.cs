using Assets.Classes;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class CompanyPreviewView : View,
    IProductListener
{
    public GameEntity _entity;
    public Text CompanyNameLabel;
    public Text CompanyTypeLabel;

    public Text ShareCostLabel;

    public void SetEntity(GameEntity entity)
    {
        _entity = entity;
        _entity.AddProductListener(this);

        Render(_entity);
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
using Assets.Classes;
using UnityEngine;
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

        Render(_entity);
    }

    void Start()
    {
        Debug.Log($"RegisterListeners CompanyPreviewView");

        _entity.AddProductListener(this);
    }

    string FormatCompanyType(CompanyType companyType)
    {
        switch (companyType)
        {
            case CompanyType.ProductCompany: return "Product Company";
            case CompanyType.Holding: return "Holding Company";
            case CompanyType.Corporation: return "Corporation";
            case CompanyType.FinancialGroup: return "Financial Group";
            case CompanyType.Group: return "Group of companies";

            default: return "WUT " + companyType.ToString();
        }
    }

    void RenderCompanyType(GameEntity entity)
    {
        CompanyTypeLabel.text = FormatCompanyType(entity.company.CompanyType);
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
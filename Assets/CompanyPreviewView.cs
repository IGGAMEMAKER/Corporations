using Assets.Classes;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class CompanyPreviewView : View, IEventListener, IProductListener
{
    public GameEntity _entity;
    public Text CompanyNameLabel;
    public Text CompanyTypeLabel;

    public void RegisterListeners(IEntity entity)
    {
        Debug.Log($"RegisterListeners CompanyPreviewView");

        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);
    }

    public void SetEntity(GameEntity entity)
    {
        _entity = entity;

        RenderCompanyName(_entity.product.Name);
    }

    void RenderCompanyName(string companyName)
    {
        CompanyNameLabel.text = companyName;
        CompanyTypeLabel.text = "Product company";
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, Industry industry, int productLevel, int explorationLevel, TeamResource resources)
    {
        Debug.Log($"OnProduct.");

        RenderCompanyName(name);
    }
}
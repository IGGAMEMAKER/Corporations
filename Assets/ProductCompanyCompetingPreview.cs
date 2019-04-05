using Assets.Classes;
using UnityEngine;
using UnityEngine.UI;

public class ProductCompanyCompetingPreview : View, IProductListener, IMarketingListener
{
    GameEntity Company;

    public Text Name;
    public Text Clients;
    public Text Level;
    public Image Panel;

    void OnDestroy()
    {
        Company.RemoveProductListener(this);
        Company.RemoveMarketingListener(this);
    }

    public void SetEntity(GameEntity entity)
    {
        Company = entity;

        Company.AddProductListener(this);
        Company.AddMarketingListener(this);

        if (ColorUtility.TryParseHtmlString("#FFAB04", out Color ourCompanyColor))

        if (entity.company.Id == myProductEntity.company.Id)
            Panel.color = ourCompanyColor;

        Render();
    }

    void RenderClients(uint clients)
    {
        Clients.text = clients.ToString();
    }

    void RenderProductInfo(string name, int level)
    {
        Name.text = name;
        Level.text = level + "";
    }

    void Render()
    {
        RenderProductInfo(Company.product.Name, Company.product.ProductLevel);

        RenderClients(Company.marketing.Clients);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, IndustryType industry, int productLevel, int explorationLevel, TeamResource resources)
    {
        RenderProductInfo(name, productLevel);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, uint clients, int brandPower, bool isTargetingEnabled)
    {
        RenderClients(clients);
    }
}

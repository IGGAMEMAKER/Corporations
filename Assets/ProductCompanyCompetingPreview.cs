using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductCompanyCompetingPreview : View, IProductListener, IMarketingListener
{
    GameEntity Company;

    public Text Name;
    public Text Clients;
    public Text Level;
    public Image Panel;
    public Text IsNewCompanyLabel;

    public void SetEntity(GameEntity entity)
    {
        Company = entity;

        Company.AddProductListener(this);
        Company.AddMarketingListener(this);

        ColorUtility.TryParseHtmlString(VisualConstants.COLOR_COMPANY_WHERE_I_AM_CEO, out Color ourCompanyColor);

        if (entity.isControlledByPlayer)
            Panel.color = ourCompanyColor;

        Render();
    }

    void RenderClients(long clients)
    {
        Clients.text = clients.ToString();
    }

    void RenderProductInfo(string name, int level)
    {
        Name.text = name;
        Level.text = level.ToString();
    }

    void Render()
    {
        RenderProductInfo(Company.product.Name, Company.product.ProductLevel);

        RenderClients(Company.marketing.Clients);

        IsNewCompanyLabel.gameObject.SetActive(Company.product.ProductLevel == 0);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long clients, long brandPower, bool isTargetingEnabled, Dictionary<NicheType, long> segments)
    {
        if (CurrentScreen == ScreenMode.NicheScreen)
            RenderClients(clients);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, int improvementPoints, Dictionary<NicheType, int> segments)
    {
        if (CurrentScreen == ScreenMode.NicheScreen)
            RenderProductInfo(name, productLevel);
    }
}

using Assets.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductCompanyCompetingPreview : View,
    IProductListener,
    IMarketingListener
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
        {
            Name.color = ourCompanyColor;

            Destroy(GetComponent<ClickOnMeIfNeverClicked>());
        }

        Render();
    }

    void RenderClients(GameEntity company)
    {
        if (Clients != null)
            Clients.text = ValueFormatter.Shorten(MarketingUtils.GetClients(company));
    }

    void RenderProductInfo(string name, Dictionary<UserType, int> segments)
    {
        Name.text = name;

        var improvements = ProductUtils.GetTotalImprovements(Company);

        if (Level != null)
            AnimateIfValueChanged(Level, improvements.ToString());
    }

    void Render()
    {
        RenderProductInfo(Company.company.Name, Company.product.Segments);

        RenderClients(Company);

        IsNewCompanyLabel.gameObject.SetActive(false);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, Dictionary<UserType, long> segments)
    {
        RenderClients(entity);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, NicheType niche, Dictionary<UserType, int> segments)
    {
        RenderProductInfo(entity.company.Name, segments);
    }
}

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

    void RenderProductInfo(string name, int level)
    {
        Name.text = name;

        if (Level != null)
            AnimateIfValueChanged(Level, $"{level} ()");
        //Level.text = level.ToString();
    }

    void Render()
    {
        RenderProductInfo(Company.product.Name, Company.product.ProductLevel);

        RenderClients(Company);

        IsNewCompanyLabel.gameObject.SetActive(Company.product.ProductLevel == 0);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long brandPower, Dictionary<UserType, long> segments)
    {
        //if (CurrentScreen == ScreenMode.NicheScreen)
        RenderClients(entity);
    }

    void IProductListener.OnProduct(GameEntity entity, int id, string name, NicheType niche, int productLevel, Dictionary<UserType, int> segments)
    {
        //if (CurrentScreen == ScreenMode.NicheScreen)
        RenderProductInfo(name, productLevel);
    }
}

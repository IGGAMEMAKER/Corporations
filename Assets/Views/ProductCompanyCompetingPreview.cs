using Assets.Core;
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

        ColorUtility.TryParseHtmlString(Colors.COLOR_COMPANY_WHERE_I_AM_CEO, out Color ourCompanyColor);

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
            Clients.text = Format.Minify(Marketing.GetClients(company));
    }

    void RenderProductInfo(string name)
    {
        Name.text = name;

        var improvements = Products.GetProductLevel(Company);

        if (Level != null)
            AnimateIfValueChanged(Level, improvements.ToString());
    }

    void Render()
    {
        RenderProductInfo(Company.company.Name);

        RenderClients(Company);

        IsNewCompanyLabel.gameObject.SetActive(false);
    }

    void IMarketingListener.OnMarketing(GameEntity entity, Dictionary<int, long> clients1)
    {
        RenderClients(entity);
    }

    void IProductListener.OnProduct(GameEntity entity, NicheType niche, int concept)
    {
        RenderProductInfo(entity.company.Name);
    }
}

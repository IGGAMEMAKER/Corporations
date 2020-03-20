using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ProductStateView : View
{
    public Text Clients;

    public Text Brand;
    public GameObject BrandIcon;

    GameEntity company;

    public void SetEntity(GameEntity c)
    {
        company = c;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        company = flagship;

        Render();
    }

    void Render()
    {
        if (company == null)
            return;

        return;

        var id = company.company.Id;

        var clients = Marketing.GetClients(company);
        var churn = Marketing.GetChurnRate(Q, company);
        var churnClients = Marketing.GetChurnClients(Q, id);

        var brand = (int)company.branding.BrandPower;
        var brandChange = Marketing.GetBrandChange(company, Q);


        Clients.text = Format.Minify(clients);

        var change = brandChange.Sum();
        Brand.text = $"{brand} ({Format.Sign(change)})";
        Brand.color = Visuals.GetGradientColor(0, 100, brand);



        UpdateIfNecessary(BrandIcon, company.isRelease);
        UpdateIfNecessary(Brand, company.isRelease);
    }

    void UpdateIfNecessary(MonoBehaviour mb, bool condition) => UpdateIfNecessary(mb.gameObject, condition);
    void UpdateIfNecessary(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductOnMarketView : View
{
    public Text Clients;
    public Text NewClients;
    public Hint ClientChange;
    public LinkToProjectView LinkToProjectView;

    public Text Brand;

    public RectTransform BackgroundRect;
    public RectTransform ClientsRect;
    public RectTransform NewClientsRect;

    public Text Growth;

    public Text Innovations;
    public Text Speed;

    public Text ProductLevel;

    public Text Name;

    int companyId;

    void Render(long maxClients)
    {
        var company = Companies.Get(Q, companyId);

        var clients = Marketing.GetClients(company);
        var newClients = Marketing.GetAudienceGrowth(company, Q);

        var level = Products.GetProductLevel(company);
        var levelStatus = Products.GetConceptStatus(company, Q);

        Clients.text = Format.Minify(clients); //  + 
        NewClients.text = Format.Minify(newClients);

        if (Growth != null)
            Growth.text = Format.Minify(newClients);


        Brand.text = (int)company.branding.BrandPower + "";

        // name
        var isPlayerRelated = Companies.IsRelatedToPlayer(Q, company);
        Name.text = company.company.Name; // + $" ({levelStatus})";

        if (ProductLevel != null)
            ProductLevel.text = $"{level}LVL";

        if (Innovations != null)
            Innovations.text = Products.GetInnovationChance(company, Q) + "%";

        if (Speed != null)
            Speed.text = Products.GetConceptUpgradeEffeciency(Q, company) + "%";

        var nameColor = isPlayerRelated ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO;
        Name.color = Visuals.GetColorFromString(nameColor);

        // link to project
        LinkToProjectView.CompanyId = company.company.Id;

        var brand = (int)company.branding.BrandPower;
        ClientChange.SetHint($"{company.company.Name} will get {Format.Minify(newClients)} clients next week, due to their brand power ({brand})");


        // scale this view according to market share
        var scale = clients * 1D / maxClients;

        //BackgroundRect.localScale = new Vector3(1, (float)scale, 1);
        //BackgroundRect.rect.height = 300 * clients / maxClients;

        var progress = GetComponent<DrawConceptProgress>();
        if (progress != null)
            progress.SetEntity(company);
    }

    public void SetEntity(int companyId, long maxClients)
    {
        this.companyId = companyId;

        Render(maxClients);
    }
}

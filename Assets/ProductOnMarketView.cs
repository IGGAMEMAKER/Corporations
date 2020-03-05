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

    public RectTransform BackgroundRect;
    public RectTransform ClientsRect;
    public RectTransform NewClientsRect;

    public Text Name;

    int companyId;

    void Render(long maxClients)
    {
        var company = Companies.Get(Q, companyId);

        var clients = Marketing.GetClients(company);
        var newClients = Marketing.GetAudienceGrowth(company, Q);

        Clients.text = Format.Minify(clients);
        NewClients.text = Format.Minify(newClients);

        var isPlayerRelated = Companies.IsRelatedToPlayer(Q, company);
        Name.text = company.company.Name;
        Name.color = Visuals.GetColorFromString(isPlayerRelated ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);

        LinkToProjectView.CompanyId = company.company.Id;

        var brand = (int)company.branding.BrandPower;
        ClientChange.SetHint($"{company.company.Name} will get this amount of clients next week, due to their brand power ({brand})");


        // scale this view according to market share
        var scale = clients * 100D / maxClients;

        BackgroundRect.localScale = new Vector3(1, (float)scale, 1);
        //BackgroundRect.rect.height = 300 * clients / maxClients;
    }

    public void SetEntity(int companyId, long maxClients)
    {
        this.companyId = companyId;

        Render(maxClients);
    }
}

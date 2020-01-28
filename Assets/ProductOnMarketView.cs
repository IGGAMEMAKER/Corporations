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

    public Text Name;

    int companyId;

    public override void ViewRender()
    {
        base.ViewRender();
        //Clients.text = 
    }

    void Render()
    {
        var company = Companies.Get(GameContext, companyId);

        var clients = Marketing.GetClients(company);
        var newClients = Marketing.GetAudienceGrowth(company, GameContext);

        Clients.text = Format.Minify(clients);
        NewClients.text = Format.Minify(newClients);

        var isPlayerRelated = Companies.IsRelatedToPlayer(GameContext, company);
        Name.text = company.company.Name;
        Name.color = Visuals.GetColorFromString(isPlayerRelated ? Colors.COLOR_COMPANY_WHERE_I_AM_CEO : Colors.COLOR_COMPANY_WHERE_I_AM_NOT_CEO);

        LinkToProjectView.CompanyId = company.company.Id;

        ClientChange.SetHint($"{company.company.Name} will get this amount of clients next week");
    }

    public void SetEntity(int companyId)
    {
        this.companyId = companyId;

        Render();
    }
}

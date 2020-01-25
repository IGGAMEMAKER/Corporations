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

    public Text Name;

    int companyId;

    public override void ViewRender()
    {
        base.ViewRender();
        //Clients.text = 
    }

    void Render()
    {
        var company = Companies.GetCompany(GameContext, companyId);

        var clients = MarketingUtils.GetClients(company);
        var newClients = 15000;

        Clients.text = Format.Minify(clients);
        NewClients.text = Format.Minify(newClients);

        Name.text = company.company.Name;

        ClientChange.SetHint($"{company.company.Name} will get this amount of clients next week");
    }

    public void SetEntity(int companyId)
    {
        this.companyId = companyId;

        Render();
    }
}

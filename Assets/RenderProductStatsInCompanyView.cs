using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductStatsInCompanyView : View
{
    public Text Clients;

    public GameObject MarketShare;

    public void Render(GameEntity company)
    {
        Clients.text = Format.Minify(Marketing.GetClients(company));

        Draw(MarketShare, company.company.Id == Flagship.company.Id);
    }
}

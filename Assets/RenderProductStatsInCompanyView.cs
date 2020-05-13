using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderProductStatsInCompanyView : MonoBehaviour
{
    public Text Clients;

    public void Render(GameEntity company)
    {
        Clients.text = Format.Minify(Marketing.GetClients(company));
    }
}

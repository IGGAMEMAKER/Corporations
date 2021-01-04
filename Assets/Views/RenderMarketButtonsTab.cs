using Assets.Core;
using UnityEngine;

public class RenderMarketButtonsTab : View
{
    public GameObject Competitors;
    public GameObject Funds;
    public GameObject Info;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasProducts = Companies.IsHasDaughters(MyCompany);

        Funds.SetActive(hasProducts);
        Info.SetActive(hasProducts);
    }
}

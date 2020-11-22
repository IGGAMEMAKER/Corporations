using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCreateProductButtonIfCreatedAlready : View
{
    public GameObject CreateProductButton;

    public override void ViewRender()
    {
        base.ViewRender();

        var niche = SelectedNiche;

        Draw(CreateProductButton, !Companies.HasCompanyOnMarket(MyCompany, niche, Q));
    }
}

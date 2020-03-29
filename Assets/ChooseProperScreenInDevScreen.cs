using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseProperScreenInDevScreen : View
{
    public GameObject NonReleasedScreen;
    public GameObject ReleasedScreen;
    public GameObject IndustrialScreen;
    public GameObject MissionScreen;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var numberOfDaughters = Companies.GetDaughterCompaniesAmount(MyCompany, Q);

        NonReleasedScreen.SetActive(!hasReleasedProducts);

        ReleasedScreen.SetActive(numberOfDaughters == 1 && hasReleasedProducts);
        // TODO also check if products are in same industry
        IndustrialScreen.SetActive(numberOfDaughters > 1 && hasReleasedProducts);

        // check company goal here
        MissionScreen.SetActive(false);
    }
}

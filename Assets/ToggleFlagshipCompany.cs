using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlagshipCompany : IterateOverCompaniesButtonController
{
    public override void Execute()
    {
        base.Execute();

        var daughters = GetEntities();

        foreach (var d in daughters)
            d.isFlagship = false;

        SelectedCompany.isFlagship = true;
    }

    public override GameEntity[] GetEntities()
    {
        return Companies.GetDaughterProductCompanies(Q, MyCompany);
    }

    public override ScreenMode GetScreenMode()
    {
        return ScreenMode.ProjectScreen;
    }
}

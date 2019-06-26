using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyBalanceChange : View
{
    public ColoredValuePositiveOrNegative Change;

    public override void ViewRender()
    {
        base.ViewRender();

        Change.UpdateValue(CompanyEconomyUtils.GetBalanceChange(SelectedCompany, GameContext));
    }
}

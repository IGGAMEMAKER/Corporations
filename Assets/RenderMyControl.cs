using Assets.Core;
using UnityEngine;

public class RenderMyControl : ParameterView
{
    public override string RenderValue()
    {
        var shareholderId = Me.shareholder.Id;
        var control = Companies.GetShareSize(GameContext, MyCompany.company.Id, shareholderId);

        Colorize(control, 0, 100);

        return Mathf.Floor(control) + "%";
    }
}

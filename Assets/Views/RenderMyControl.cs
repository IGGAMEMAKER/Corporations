using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderMyControl : ParameterView
{
    public override string RenderValue()
    {
        var shareholderId = Hero.shareholder.Id;
        var control = Companies.GetShareSize(Q, MyCompany.company.Id, shareholderId);

        Colorize(control, 0, 100);

        return Mathf.Floor(control) + "%";
    }
}

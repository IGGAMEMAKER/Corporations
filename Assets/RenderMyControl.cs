using Assets.Utils;
using UnityEngine;

public class RenderMyControl : ParameterView
{
    public override string RenderValue()
    {
        var shareholderId = Me.shareholder.Id;
        var control = Companies.GetShareSize(GameContext, MyCompany.company.Id, shareholderId);

        return Mathf.Floor(control) + "%";
    }
}

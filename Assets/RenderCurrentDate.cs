using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCurrentDate : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var date = CurrentIntDate;

        var dateDescription = Format.GetDateDescription(date);

        return (dateDescription.day + 1) + "\n" + dateDescription.monthLiteral;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderProductUpgrades : ParameterView
{
    public override string RenderValue()
    {
        var upgrades = Flagship.productUpgrades.upgrades;

        var text = "";

        foreach (var up in upgrades)
        {
            if (!up.Value)
                continue;

            text += $"{up.Key} is active\n";
        }

        return text;
    }
}

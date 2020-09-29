using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLoadView : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var load = Products.GetServerLoad(Flagship);
        var capacity = Products.GetServerCapacity(Flagship);

        var loadFormatted = Format.Minify(load);
        var capacityFormatted = Format.Minify(capacity);

        bool overloaded = Products.IsNeedsMoreServers(Flagship);

        var percentage = 100L;

        if (capacity > 0)
            percentage = load * 100 / capacity;

        var str = $"Load: <b>{loadFormatted} / {capacityFormatted} ({Visuals.Colorize((int)percentage, 0, 100, true)}%)</b>";

        if (overloaded)
        {
            str += " (" + Visuals.Negative("ADD SERVERS!") + ")";
        }

        return str;
    }
}

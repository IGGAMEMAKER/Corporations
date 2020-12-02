using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceLossView : ParameterView
{
    public override string RenderValue()
    {
        var loss = Marketing.GetChurnClients(Flagship);

        if (loss > 0)
            return $"We are {Visuals.Negative("LOSING")} {Format.Minify(loss)} users weekly";

        return "";
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipUsers : ParameterView
{
    public override string RenderValue()
    {
        var clients = Marketing.GetUsers(Flagship);

        return Format.Minify(clients) + " users";
    }
}

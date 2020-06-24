using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderFlagshipAudience : ParameterDynamicView
{
    public override long GetParameter()
    {
        return Marketing.GetClients(Flagship);
    }
}

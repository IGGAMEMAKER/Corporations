using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceGainFromChannelsView : ParameterView
{
    public override string RenderValue()
    {
        var gain = Marketing.GetAudienceGrowth(Flagship, Q);

        if (gain > 0)
        {
            return $"We get {Visuals.Positive(Format.Minify(gain))} users from channels";
        }

        return $"We get {Visuals.Negative("ZERO")} users now.\n\nAdd more channels to get users!";
    }
}

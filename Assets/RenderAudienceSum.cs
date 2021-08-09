using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderAudienceSum : ParameterView
{
    public override string RenderValue()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

        var users = competitors.Sum(c => Marketing.GetUsers(c));
        var avgChurn = competitors.Average(c => Marketing.GetChurnRate(c, Q));

        return "Competitors " + Format.Minify(users) + $" users {avgChurn.ToString("0.0")}%";
    }
}

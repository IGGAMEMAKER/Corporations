using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTeamAverageStrength : ParameterView
{
    public override string RenderValue()
    {
        var managers = Flagship.team.Managers;

        if (managers.Count == 0)
            return "0";

        int rating = 0;

        foreach (var m in managers)
        {
            rating += Humans.GetRating(Q, m.Key);
        }

        var avg = rating / managers.Count;

        return avg.ToString() + "LVL";
    }
}

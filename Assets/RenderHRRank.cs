using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHRRank : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Rank.ToString();
    }

    public override string RenderValue()
    {
        return Rank.Sum() + "LVL";
    }

    Bonus<int> Rank => Teams.GetHRBasedNewManagerRatingBonus(Flagship, Q);
}

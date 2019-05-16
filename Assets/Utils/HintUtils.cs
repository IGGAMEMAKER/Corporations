﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct BonusDescription
{
    public long Value;

    public string Name;

    public bool HideIfZero;
}

public class BonusContainer
{
    public List<BonusDescription> bonusDescriptions;
    public BonusDescription parameter;

    public BonusContainer(BonusDescription bonusDescription) {
        bonusDescriptions = new List<BonusDescription>();
        parameter = bonusDescription;
    }

    public BonusContainer(string bonusName, long bonusDescription) {
        bonusDescriptions = new List<BonusDescription>();

        parameter = new BonusDescription { Name = bonusName, Value = bonusDescription };
    }

    public BonusContainer Append(BonusDescription bonus)
    {
        bonusDescriptions.Add(bonus);

        return this;
    }

    public BonusContainer Append(string bonusName, long value)
    {
        return Append(new BonusDescription { Name = bonusName, Value = value });
    }

    public BonusContainer Build()
    {
        long sum = 0;

        foreach (var bonus in bonusDescriptions)
            sum += bonus.Value;

        var unknown = new BonusDescription { Name = "UNKNOWN DATA", HideIfZero = true, Value = sum - parameter.Value };

        if (unknown.Value > 0)
            Debug.Log("UNKNOWN DATA in " + parameter.Name);

        bonusDescriptions.Insert(0, unknown);

        return this;
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();

        str.AppendLine();

        Build();

        foreach (var bonus in bonusDescriptions)
        {
            if (!bonus.HideIfZero)
                str.AppendLine(VisualUtils.Describe(bonus));
        }

        return str.ToString();
    }
}

namespace Assets.Utils
{
    public static class HintUtils
    {
    }
}

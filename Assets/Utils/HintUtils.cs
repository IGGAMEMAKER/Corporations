using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct BonusDescription
{
    public long Value;

    public string Name;

    public bool HideIfZero;

    public string Dimension;
}

public class BonusContainer
{
    public List<BonusDescription> bonusDescriptions;
    public BonusDescription parameter;

    public bool renderTitle;
    public string dimension;

    public BonusContainer(BonusDescription bonusDescription, bool renderTitle = false) {
        bonusDescriptions = new List<BonusDescription>();
        this.renderTitle = renderTitle;

        parameter = bonusDescription;
    }

    public BonusContainer(string bonusName, long bonusDescription, bool renderTitle = false) {
        bonusDescriptions = new List<BonusDescription>();
        this.renderTitle = renderTitle;

        parameter = new BonusDescription { Name = bonusName, Value = bonusDescription };
    }

    public BonusContainer SetDimension(string dim)
    {
        dimension = dim;

        return this;
    }

    public BonusContainer Append(BonusDescription bonus)
    {
        bonusDescriptions.Add(bonus);

        return this;
    }

    public BonusContainer Append(string bonusName, long value, string dimension = "")
    {
        return Append(new BonusDescription { Name = bonusName, Value = value, Dimension = dimension });
    }

    public long Sum()
    {
        long sum = 0;

        foreach (var bonus in bonusDescriptions)
            sum += bonus.Value;

        return sum;
    }

    public BonusContainer Build()
    {
        var sum = Sum();

        var unknown = new BonusDescription { Name = "UNKNOWN DATA", HideIfZero = true, Value = parameter.Value - sum };

        if (unknown.Value > 0)
            Debug.Log("UNKNOWN DATA in " + parameter.Name);

        bonusDescriptions.Insert(0, unknown);

        return this;
    }

    public string ToString(bool positiveIsNegative = false)
    {
        StringBuilder str = new StringBuilder();

        str.AppendLine("\n** Based on **");

        Build();

        foreach (var bonus in bonusDescriptions)
        {
            if (!(bonus.HideIfZero && bonus.Value == 0))
                str.AppendLine(VisualUtils.Describe(bonus.Name, bonus.Value, bonus.Dimension + dimension, positiveIsNegative));
        }

        return str.ToString();
    }
}

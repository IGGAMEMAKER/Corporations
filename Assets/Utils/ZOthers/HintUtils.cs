using Assets.Utils;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum BonusType
{
    Additive,
    Multiplicative
}

public struct BonusDescription
{
    public long Value;

    public string Name;

    public bool HideIfZero;

    public BonusType BonusType;

    public string Dimension;
}

public class BonusContainer
{
    public List<BonusDescription> bonusDescriptions;
    public string parameter;

    public bool renderTitle;
    public string dimension;

    bool isCapped = false;
    long capMin = 0;
    long capMax = 0;

    public BonusContainer(string bonusName) {
        bonusDescriptions = new List<BonusDescription>();

        parameter = bonusName;
    }

    public BonusContainer SetDimension(string dim)
    {
        dimension = dim;

        return this;
    }

    public BonusContainer RenderTitle()
    {
        renderTitle = true;

        return this;
    }

    public BonusContainer Append(BonusDescription bonus)
    {
        bonus.BonusType = BonusType.Additive;
        bonusDescriptions.Add(bonus);

        return this;
    }

    public BonusContainer Multiply(BonusDescription bonus)
    {
        bonus.BonusType = BonusType.Multiplicative;
        bonusDescriptions.Add(bonus);

        return this;
    }

    public BonusContainer AppendAndHideIfZero(string bonusName, long value, string dimension = "")
    {
        return Append(new BonusDescription { Name = bonusName, Dimension = dimension, HideIfZero = true, Value = value });
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

        if (isCapped)
            return (long)Mathf.Clamp(sum, capMin, capMax);

        return sum;
    }

    public string ToString(bool positiveIsNegative = false)
    {
        StringBuilder str = new StringBuilder();

        long val = Sum();

        if (renderTitle)
            str.AppendFormat("{0} is {1}", parameter, Format.Sign(val));

        str.AppendLine("\n** Based on **\n");

        foreach (var bonus in bonusDescriptions)
        {
            if (bonus.HideIfZero && bonus.Value == 0)
                continue;

            var text = "";

            //if (minify)
            text = Visuals.Describe(bonus.Name, bonus.Value, bonus.Dimension + dimension, positiveIsNegative, bonus.BonusType);

            str.AppendLine(text);
        }



        return str.ToString();
    }
}

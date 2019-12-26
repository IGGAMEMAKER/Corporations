using Assets.Core;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum BonusType
{
    Additive,
    Multiplicative
}

public struct BonusDescription<T>
{
    public T Value;

    public string Name;

    public bool HideIfZero;

    public BonusType BonusType;

    public string Dimension;
}

public class Bonus<T>
{
    public List<BonusDescription<T>> bonusDescriptions;
    public string parameter;

    public bool renderTitle;
    public string dimension;

    bool isCapped = false;
    long capMin = 0;
    long capMax = 0;

    bool renderSubTitle = true;
    bool minifyValues = false;

    public Bonus(string bonusName) {
        bonusDescriptions = new List<BonusDescription<T>>();

        parameter = bonusName;
    }

    public Bonus<T> Minify()
    {
        renderSubTitle = false;

        return this;
    }

    public Bonus<T> MinifyValues()
    {
        minifyValues = true;

        return this;
    }

    public Bonus<T> Cap(long min, long max)
    {
        capMin = min;
        capMax = max;

        isCapped = true;

        return this;
    }

    public Bonus<T> SetDimension(string dim)
    {
        dimension = dim;

        return this;
    }

    public Bonus<T> RenderTitle()
    {
        renderTitle = true;

        return this;
    }

    public Bonus<T> AppendAndHideIfZero(string bonusName, T value, string dimension = "") => Append(new BonusDescription<T> { Name = bonusName, Dimension = dimension, HideIfZero = true, Value = value });
    public Bonus<T> Append(string bonusName, T value, string dimension = "") => Append(new BonusDescription<T> { Name = bonusName, Value = value, Dimension = dimension });
    private Bonus<T> Append(BonusDescription<T> bonus)
    {
        bonus.BonusType = BonusType.Additive;
        bonusDescriptions.Add(bonus);

        return this;
    }



    public Bonus<T> MultiplyAndHideIfOne(string bonusName, T value, string dimension = "") => Multiply(new BonusDescription<T> { Name = bonusName, Dimension = dimension, HideIfZero = true, Value = value });
    public Bonus<T> Multiply(string bonusName, T value, string dimension = "") => Multiply(new BonusDescription<T> { Name = bonusName, Value = value, Dimension = dimension });
    public Bonus<T> Multiply(BonusDescription<T> bonus)
    {
        bonus.BonusType = BonusType.Multiplicative;
        bonusDescriptions.Add(bonus);

        return this;
    }

    public long Sum()
    {
        long sum = 0;

        foreach (var bonus in bonusDescriptions)
        {
            if (bonus.BonusType == BonusType.Multiplicative)
                sum *= (long)(object)bonus.Value;
            else
                sum += (long)(object)bonus.Value;
        }

        if (isCapped)
        {
            return (long)Mathf.Clamp(sum, capMin, capMax);
        }

        return sum;
    }

    public string ToString(bool positiveIsNegative = false)
    {
        StringBuilder str = new StringBuilder();

        long val = Sum();

        if (renderTitle)
            str.AppendFormat("{0} is {1}", parameter, Format.Sign(val));

        if (renderSubTitle)
            str.AppendLine("\n** Based on **\n");

        foreach (var bonus in bonusDescriptions)
        {
            long value = (long)(object)bonus.Value;

            if (bonus.HideIfZero)
            {
                if (bonus.BonusType == BonusType.Additive && value == 0)
                    continue;

                if (bonus.BonusType == BonusType.Multiplicative && value == 1)
                    continue;
            }

            var text = "";

            text = Visuals.RenderBonus(bonus.Name, value, bonus.Dimension + dimension, positiveIsNegative, bonus.BonusType, minifyValues);

            str.AppendLine(text);
        }

        if (isCapped)
        {
            if (val == capMin || val == capMax)
                str.AppendLine("Value capped at " + val);
        }

        return str.ToString();
    }
}

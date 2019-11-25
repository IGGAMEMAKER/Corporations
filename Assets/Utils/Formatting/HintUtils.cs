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

public class Bonus
{
    public List<BonusDescription> bonusDescriptions;
    public string parameter;

    public bool renderTitle;
    public string dimension;

    bool isCapped = false;
    long capMin = 0;
    long capMax = 0;

    bool renderSubTitle = true;
    bool minifyValues = false;

    public Bonus(string bonusName) {
        bonusDescriptions = new List<BonusDescription>();

        parameter = bonusName;
    }

    public Bonus Minify()
    {
        renderSubTitle = false;

        return this;
    }

    public Bonus MinifyValues()
    {
        minifyValues = true;

        return this;
    }

    public Bonus Cap(long min, long max)
    {
        capMin = min;
        capMax = max;

        isCapped = true;

        return this;
    }

    public Bonus SetDimension(string dim)
    {
        dimension = dim;

        return this;
    }

    public Bonus RenderTitle()
    {
        renderTitle = true;

        return this;
    }

    public Bonus AppendAndHideIfZero(string bonusName, long value, string dimension = "") => Append(new BonusDescription { Name = bonusName, Dimension = dimension, HideIfZero = true, Value = value });
    public Bonus Append(string bonusName, long value, string dimension = "") => Append(new BonusDescription { Name = bonusName, Value = value, Dimension = dimension });
    private Bonus Append(BonusDescription bonus)
    {
        bonus.BonusType = BonusType.Additive;
        bonusDescriptions.Add(bonus);

        return this;
    }



    public Bonus MultiplyAndHideIfOne(string bonusName, long value, string dimension = "") => Multiply(new BonusDescription { Name = bonusName, Dimension = dimension, HideIfZero = true, Value = value });
    public Bonus Multiply(string bonusName, long value, string dimension = "") => Multiply(new BonusDescription { Name = bonusName, Value = value, Dimension = dimension });
    public Bonus Multiply(BonusDescription bonus)
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
                sum *= bonus.Value;
            else
                sum += bonus.Value;
        }

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

        if (renderSubTitle)
            str.AppendLine("\n** Based on **\n");

        foreach (var bonus in bonusDescriptions)
        {
            if (bonus.HideIfZero)
            {
                if (bonus.BonusType == BonusType.Additive && bonus.Value == 0)
                    continue;

                if (bonus.BonusType == BonusType.Multiplicative && bonus.Value == 1)
                    continue;
            }

            var text = "";

            text = Visuals.RenderBonus(bonus.Name, bonus.Value, bonus.Dimension + dimension, positiveIsNegative, bonus.BonusType, minifyValues);

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

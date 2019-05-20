using System.Collections.Generic;
using System.Text;

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
    public string parameter;

    public bool renderTitle;
    public string dimension;

    public BonusContainer(string bonusName, bool renderTitle = false) {
        bonusDescriptions = new List<BonusDescription>();
        this.renderTitle = renderTitle;

        parameter = bonusName;
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

        return sum;
    }

    public string ToString(bool positiveIsNegative = false)
    {
        StringBuilder str = new StringBuilder();

        str.AppendLine("\n** Based on **");

        foreach (var bonus in bonusDescriptions)
        {
            if (!(bonus.HideIfZero && bonus.Value == 0))
                str.AppendLine(VisualUtils.Describe(bonus.Name, bonus.Value, bonus.Dimension + dimension, positiveIsNegative));
        }

        return str.ToString();
    }
}

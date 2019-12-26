using Assets.Core;
using System;

public abstract class IndustryNavigator: ButtonController
{
    public abstract int Tweak(int index);

    public override void Execute()
    {
        IndustryType industryType = ScreenUtils.GetSelectedIndustry(GameContext);

        var c = Enum.GetValues(typeof(IndustryType));

        int index = Array.IndexOf(c, industryType);

        int next = Tweak(index);

        if (next >= c.Length)
            next = 0;

        if (next < 0)
            next = c.Length - 1;

        NavigateToIndustry((IndustryType) c.GetValue(next));
    }
}

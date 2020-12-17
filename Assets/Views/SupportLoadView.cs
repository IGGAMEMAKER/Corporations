using Assets.Core;

public class SupportLoadView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var load = Format.Minify(Products.GetSupportLoad(Flagship));
        var capacity = Format.Minify(Products.GetSupportCapacity(Flagship));

        bool overloaded = Products.IsNeedsMoreMarketingSupport(Flagship);

        var str = $"Load: <b>{load} / {capacity}</b>";

        if (overloaded)
        {
            str += Visuals.Negative(" Support is overloaded!");
        }

        return str;
    }
}

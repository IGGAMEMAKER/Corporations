using Assets.Core;
using UnityEngine;

// #Culture
public class RenderInvestorCorporateCulture : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        string text = "";

        if (SelectedInvestor == null || !SelectedInvestor.hasCorporateCulture)
            return "";

        text += Wrap(CorporatePolicy.Make,                  "", "Favors innovative companies");
        text += Wrap(CorporatePolicy.FocusingOrSpread,      "Focuses on one market/product/industry",   "Likes to diversify risks");
        text += Wrap(CorporatePolicy.Sell,                  "",  "Favors popular companies");
        text += Wrap(CorporatePolicy.CompetitionOrSupport,  "Be best", "Favors stable companies");

        return text.Length == 0 ? "No specific preferences" : text;
    }

    string Wrap(CorporatePolicy corporatePolicy, string descriptionLeft, string descriptionRight)
    {
        var investorCulture = Companies.GetOwnCulture(SelectedInvestor);
        var playerCulture = Companies.GetActualCorporateCulture(MyCompany);


        int diff = Mathf.Abs(investorCulture[corporatePolicy] - playerCulture[corporatePolicy]);

        string description = DescribePolicy(corporatePolicy, descriptionLeft, descriptionRight);

        if (description.Length > 0)
        {
            var colorName = Colors.COLOR_NEUTRAL;

            if (diff <= 2)
                colorName = Colors.COLOR_POSITIVE;

            if (diff > 6)
                colorName = Colors.COLOR_NEGATIVE;

            return Visuals.Colorize(description + "\n", colorName);
        }

        return "";
    }

    string DescribePolicy(CorporatePolicy corporatePolicy, string left, string right)
    {
        var investorCulture = Companies.GetOwnCulture(SelectedInvestor);

        var val = investorCulture[corporatePolicy];


        if (val == 5)
            return right + "\n";

        if (val == 0)
            return left + "\n";

        return "";
    }
}

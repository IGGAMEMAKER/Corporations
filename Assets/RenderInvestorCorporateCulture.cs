using Assets.Core;
using UnityEngine;

public class RenderInvestorCorporateCulture : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        string text = "";

        if (SelectedInvestor == null || !SelectedInvestor.hasCorporateCulture)
            return "";

        text += Wrap(CorporatePolicy.BuyOrCreate,   DescribePolicy(CorporatePolicy.BuyOrCreate,     "Loves acquisitions", "Loves creating new products"));
        text += Wrap(CorporatePolicy.Focusing,      DescribePolicy(CorporatePolicy.Focusing,        "Focuses on one market/product/industry",   "Doesn't focus in one market/industry"));
        text += Wrap(CorporatePolicy.LeaderOrTeam,  DescribePolicy(CorporatePolicy.LeaderOrTeam,    "Favors manager-centric companies",     "Favors team oriented companies"));
        text += Wrap(CorporatePolicy.WorkerMindset, DescribePolicy(CorporatePolicy.WorkerMindset,   "Favors innovative companies",               "Favors stable companies"));

        return text.Length == 0 ? "No specific preferences" : text;
    }

    string Wrap(CorporatePolicy corporatePolicy, string description)
    {
        var investorCulture = Companies.GetOwnCorporateCulture(SelectedInvestor);
        var playerCulture = Companies.GetActualCorporateCulture(MyCompany, Q);


        int diff = Mathf.Abs(investorCulture[corporatePolicy] - playerCulture[corporatePolicy]);

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
        var investorCulture = Companies.GetOwnCorporateCulture(SelectedInvestor);

        var val = investorCulture[corporatePolicy];


        if (val == 5)
            return right + "\n";

        if (val == 0)
            return left + "\n";

        return "";
    }
}

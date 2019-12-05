using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderInvestorCorporateCulture : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        string text = "";

        text += DescribePolicy(CorporatePolicy.CreateOrBuy,     "Loves acquisitions",               "Loves creating new products");
        text += DescribePolicy(CorporatePolicy.Focusing,        "Focuses on one market/industry",   "Doesn't focus in one market/industry");
        text += DescribePolicy(CorporatePolicy.Responsibility,  "Favors centralised companies",     "Favors decentralised companies");
        text += DescribePolicy(CorporatePolicy.WorkerMindset,   "Favors researchers",               "Favors engineers");

        return text.Length == 0 ? "No specific preferences" : text;
    }

    string DescribePolicy(CorporatePolicy corporatePolicy, string left, string right)
    {
        var investorCulture = SelectedInvestor.corporateCulture.Culture;

        var val = investorCulture[corporatePolicy];




        if (val == 5)
            return right + "\n";

        if (val == 0)
            return left + "\n";

        return "";
    }
}

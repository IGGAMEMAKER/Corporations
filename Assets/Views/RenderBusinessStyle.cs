using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #Culture
public class RenderBusinessStyle : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var culture = Companies.GetActualCorporateCulture(SelectedCompany, Q);

        var text = "";

        text += Wrap(DescribePolicy(culture, CorporatePolicy.CompetitionOrSupport)) + "\n\n";
        text += Wrap(DescribePolicy(culture, CorporatePolicy.FocusingOrSpread));
        text += Wrap(DescribePolicy(culture, CorporatePolicy.Make));

        //foreach (var c in culture)
        //{
        //    var policy = c.Key;
        //    var value = c.Value;

        //    text += Wrap(DescribePolicy(value, policy));
        //}

        if (text.Length == 0)
            text = "This company has no specific preferences in management";

        return text;
    }

    string Wrap(string description)
    {
        if (description.Length > 0)
            return description + "\n";

        return "";
    }

    string DescribePolicy(Dictionary<CorporatePolicy, int> culture, CorporatePolicy policy)
    {
        return DescribePolicy(culture[policy], policy);
    }

    string DescribePolicy(int value, CorporatePolicy policy)
    {
        bool isLeft = value <= 2;
        bool isRight = value >= 8;

        if (!isLeft && !isRight)
            return "";

        switch (policy)
        {
            case CorporatePolicy.Make: return DescribeAcquisitionPolicy(isLeft);
            case CorporatePolicy.FocusingOrSpread: return DescribeFocusingPolicy(isLeft);
            case CorporatePolicy.SalariesLowOrHigh: return DescribeSalaries(isLeft);
            case CorporatePolicy.CompetitionOrSupport: return DescribeAggressiveness(isLeft);

            default: return policy.ToString() + ": " + value;
        }
    }

    string DescribeAggressiveness(bool isLeft)
    {
        if (isLeft)
            return "Aggressive business management";

        return "They love partnerships";
    }

    string DescribeMentality(bool isLeft)
    {
        if (isLeft)
            return "Innovative mentality";

        return "Stable mentality";
    }

    string DescribeSalaries(bool isLeft)
    {
        if (isLeft)
            return "Pays low salaries";

        return "Pays big salaries";
    }

    string DescribeAcquisitionPolicy(bool isLeft)
    {
        if (isLeft)
            return "They love buying companies";

        return "They prefer developing their own products";
    }

    string DescribeFocusingPolicy(bool isLeft)
    {
        if (isLeft)
            return "They prefer focusing";

        return "They prefer diversifying risks";
    }
}

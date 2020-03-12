using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBusinessStyle : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var culture = Companies.GetActualCorporateCulture(SelectedCompany, Q);

        var text = "";

        foreach (var c in culture)
        {
            var policy = c.Key;
            var value = c.Value;

            text += Wrap(DescribePolicy(value, policy));
        }

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

    string DescribePolicy(int value, CorporatePolicy policy)
    {
        bool isLeft = value <= 2;
        bool isRight = value >= 8;

        if (!isLeft && !isRight)
            return "";

        switch (policy)
        {
            case CorporatePolicy.BuyOrCreate: return DescribeAcquisitionPolicy(isLeft);
            case CorporatePolicy.FocusingOrSpread: return DescribeFocusingPolicy(isLeft);
            case CorporatePolicy.LeaderOrTeam: return DescribeLeadership(isLeft);
            case CorporatePolicy.SalariesLowOrHigh: return DescribeSalaries(isLeft);
            case CorporatePolicy.InnovationOrStability: return DescribeMentality(isLeft);

            default: return policy.ToString() + ": " + value;
        }
    }

    string DescribeMentality(bool isLeft)
    {
        if (isLeft)
            return "Innovative mentality";

        return "Stable mentality";
    }

    string DescribeSalaries(bool isLeft)
    {
        return "Salaries??";
    }
    string DescribeLeadership(bool isLeft)
    {
        if (isLeft)
            return "Manager oriented company";

        return "Team oriented company";
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

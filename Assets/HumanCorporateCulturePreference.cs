using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCorporateCulturePreference : ParameterView
{
    public override string RenderValue()
    {
        var human = SelectedHuman;
        var culture = human.corporateCulture.Culture;

        var company = Companies.Get(Q, human.worker.companyId);

        var isEmployed = human.worker.companyId >= 0;

        //Debug.Log($"Corporate culture preference: #{human.creationIndex} {human.worker.companyId}");
        var companyCulture = Companies.GetActualCorporateCulture(MyCompany, Q);

        var text = "";

        foreach (var c in culture)
        {
            var policy = c.Key;
            var value = c.Value;

            var companyPolicy = companyCulture[policy];

            text += Wrap(DescribePolicy(value, policy), Mathf.Abs(value - companyPolicy));
        }

        if (text.Length == 0)
            text = "Doesn't care, where to work";

        return text;
    }

    string Wrap(string description, int diff)
    {
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
            case CorporatePolicy.CompetitionOrSupport: return DescribeAttitudeToCompetition(isLeft);

            default: return policy.ToString() + ": " + value;
        }
    }

    string DescribeAttitudeToCompetition(bool isLeft)
    {
        if (isLeft)
            return "Prefers competitive atmosphere";

        return "Prefers collaborative teams";
    }

    string DescribeMentality(bool isLeft)
    {
        if (isLeft)
            return "Loves innovative companies";

        return "Loves stable companies";
    }

    string DescribeSalaries(bool isLeft)
    {
        return "Salaries??";
    }

    string DescribeLeadership(bool isLeft)
    {
        if (isLeft)
            return "Loves making decisions";

        return "Loves work in team";
    }

    string DescribeAcquisitionPolicy(bool isLeft)
    {
        if (isLeft)
            return "Prefers sales oriented companies";

        return "Prefers creating new products";
    }

    string DescribeFocusingPolicy(bool isLeft)
    {
        if (isLeft)
            return "Prefers to focus in one market";

        return "Likes new challenges";
    }
}

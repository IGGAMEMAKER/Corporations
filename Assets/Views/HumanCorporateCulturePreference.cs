using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #Culture
public class HumanCorporateCulturePreference : ParameterView
{
    public override string RenderValue()
    {
        var human = SelectedHuman;
        var culture = human.corporateCulture.Culture;

        var company = Companies.Get(Q, human.worker.companyId);

        var isEmployed = human.worker.companyId >= 0;

        //Debug.Log($"Corporate culture preference: #{human.creationIndex} {human.worker.companyId}");
        var playerCulture = Companies.GetActualCorporateCulture(MyCompany, Q);

        var text = "";

        var importantPolicies = Teams.GetImportantCorporatePolicies();
        foreach (var c in importantPolicies)
        {
            var policy = c;
            var value = culture[policy];

            var companyPolicy = playerCulture[policy];

            text += Wrap(DescribePolicy(value, policy), Mathf.Abs(value - companyPolicy));
        }

        if (text.Length == 0)
            text = "Doesn't care, where to work";

        var change = Teams.GetLoyaltyChangeForManager(human, playerCulture);
        text += "\n\n";

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, Flagship.company.Id);

        if (isEmployed && worksInMyCompany)
        {
            // TODO copypasted in HumanPreview.cs
            text += Visuals.DescribeValueWithText(change,
                $"Enjoys work in this company!\n\nWeekly loyalty change: +{change}",
                $"Doesn't like this company!\n\nWeekly loyalty change: {change}",
                "Is satisfied by this company"
                );
        }

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
        return "Needs high salaries";
    }

    string DescribeLeadership(bool isLeft)
    {
        if (isLeft)
            return "Loves micromanagement";

        return "Loves freedom in making decisions";
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

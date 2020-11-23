using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamOveralKPIView : ParameterView
{
    public override string RenderValue()
    {
        var company = SelectedCompany;

        var teams = company.team.Teams.Count;
        var employees = Teams.GetTeamSize(company) * 10;
        var managerLevel = Teams.GetTeamAverageStrength(company, Q);

        var featureCap = Teams.GetMaxFeatureRatingCap(company, Q).Sum();
        var marketingEff = Teams.GetMarketingEfficiency(company);
        var devEff = Teams.GetDevelopmentEfficiency(company);



        // ----------------------------------------------------

        var text = RenderParameter("Employees", employees.ToString(), 1);

        text += RenderParameter("\nAverage manager lvl", $"{managerLevel}lvl", 2);
        text += RenderParameter("\nTeams", teams.ToString(), 1);
        text += $"\n\n--------------------";

        text += RenderParameter("\n\nMax feature lvl", featureCap.ToString("0.0"), 1);
        text += RenderParameter("\nAverage marketing efficiency", $"{marketingEff}%", 1);
        text += RenderParameter("\nAverage development efficiency", $"{devEff}%", 2);

        return text;
    }

    string RenderParameter(string name, string value, int place)
    {
        return $"{name}: {Big(value)} ({Place(place)})";
    }

    string Place(int place)
    {
        place++;

        var text = $"#{place}";

        if (place == 1)
            return Visuals.Positive(text);

        if (place == 2)
            return Visuals.Colorize(text, "yellow");

        if (place == 3)
            return Visuals.Colorize(text, "orange");

        return Visuals.Negative(text);
    }

    string Big(string txt) => $"<size=42>{txt}</size>";
}

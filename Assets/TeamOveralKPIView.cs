using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var directCompetitors = Companies.GetDirectCompetitors(company, Q, true);

        // directCompetitors.OrderByDescending(c => Teams.GetTeamAverageStrength(c, Q)).Select((c, i)=> new { c, i }).Where(
        var managerLvlPlace = Random.Range(0, 5);
        var teamsPlace = Random.Range(0, 5);
        var capPlace = Random.Range(0, 5);
        var marketingEffPlace = Random.Range(0, 5);
        var devEffPlace = Random.Range(0, 5);

        // ----------------------------------------------------

        var text = RenderParameter("Employees", $"{employees} workers", -1);

        text += RenderParameter("\nAverage manager lvl", $"{managerLevel}lvl", managerLvlPlace);
        text += RenderParameter("\nTeams", teams.ToString(), teamsPlace);
        text += $"\n\n--------------------";

        text += RenderParameter("\n\nMax feature lvl", featureCap.ToString("0.0lv"), capPlace);
        text += RenderParameter("\nAverage marketing efficiency", $"{marketingEff}%", marketingEffPlace);
        text += RenderParameter("\nAverage development efficiency", $"{devEff}%", devEffPlace);

        return text;
    }

    string RenderParameter(string name, string value, int place)
    {
        if (place >= 0)
            return $"{name}: <b>{Big(value)}</b> ({Place(place)})";

        return $"{name}: <b>{Big(value)}</b>";
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

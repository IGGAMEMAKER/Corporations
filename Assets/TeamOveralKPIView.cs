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

        if (!company.hasProduct)
            return "";

        var teams = company.team.Teams.Count;
        var employees = Teams.GetTeamSize(company);
        var managerLevel = Teams.GetTeamAverageStrength(company, Q);

        var featureCap = Teams.GetMaxFeatureRatingCap(company, Q).Sum();
        var marketingEff = Teams.GetMarketingEfficiency(company);
        var devEff = Teams.GetDevelopmentEfficiency(company);

        var directCompetitors = Companies.GetDirectCompetitors(company, Q, true);

        // directCompetitors.OrderByDescending(c => Teams.GetTeamAverageStrength(c, Q)).Select((c, i)=> new { c, i }).Where(
        var managerLvlPlace = directCompetitors.OrderByDescending(c => Teams.GetTeamAverageStrength(c, Q))
            .Select((c, i) => new { c, i }).First(r => r.c.company.Id == company.company.Id).i;

        var teamsPlace = directCompetitors.OrderByDescending(c => c.team.Teams.Count)
            .Select((c, i) => new { c, i }).First(r => r.c.company.Id == company.company.Id).i;

        var capPlace = directCompetitors.OrderByDescending(c => Teams.GetMaxFeatureRatingCap(c, Q).Sum())
            .Select((c, i) => new { c, i }).First(r => r.c.company.Id == company.company.Id).i;

        var marketingEffPlace = directCompetitors.OrderByDescending(c => Teams.GetMarketingEfficiency(c))
            .Select((c, i) => new { c, i }).First(r => r.c.company.Id == company.company.Id).i;

        var devEffPlace = directCompetitors.OrderByDescending(c => Teams.GetDevelopmentEfficiency(c))
            .Select((c, i) => new { c, i }).First(r => r.c.company.Id == company.company.Id).i;

        // ----------------------------------------------------

        var text = $"<size=50>Quantity</size>";

        text += RenderParameter("\n\nEmployees", $"{employees} workers", -1);
        text += RenderParameter("\nTeams", teams.ToString(), teamsPlace);
        
        text += $"\n\n<size=50>Quality</size>";
        
        text += RenderParameter("\n\nAverage manager lvl", $"{managerLevel}lvl", managerLvlPlace);
        text += RenderParameter("\nMax feature lvl", featureCap.ToString("0.0lv"), capPlace);
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

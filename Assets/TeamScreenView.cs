﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreenView : View
{
    public Text ManagerAdvice;

    public GameObject LeftPanel;

    public Text TeamStats;
    public Text Advices;

    public GameObject FireTeam;


    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;
        var team = product.team.Teams[SelectedTeam];

        bool hasLeadManager = Teams.HasMainManagerInTeam(team, Q, product);

        var mainManagerRole = Teams.GetMainManagerForTheTeam(team);
        var formattedManager = Humans.GetFormattedRole(mainManagerRole);

        Draw(LeftPanel, hasLeadManager);
        Draw(FireTeam, SelectedTeam > 0);

        // --------------------------------------

        var ratingGain = Products.GetFeatureRatingGain(product, team, Q);
        var marketingEffeciency = Marketing.GetMarketingTeamEffeciency(Q, product, team);
        var featureCap = Products.GetFeatureRatingCap(product, team, Q);
        var devSpeed = Products.GetBaseIterationTime(Q, product);

        var stats = new StringBuilder()
            .Append("Max feature level: ")
            .AppendLine(Visuals.Positive(featureCap.ToString("0.0lvl (")) + Visuals.Positive(Format.ShowChange(ratingGain) + "lvl)"))

            .Append("Marketing efficiency: ")
            .AppendLine(Visuals.Positive(marketingEffeciency.ToString("0") + "%"))
            ;

        //TeamStats.text = stats.ToString();
        Hide(TeamStats);

        // hiring advice ---------------------------------
        var advices = new StringBuilder();

        if (!hasLeadManager)
        {
            advices.AppendLine(Visuals.Negative($"Hire {formattedManager} to manage this team properly!"));
        }
        else
        {
            if (Teams.IsNeedsToHireRole(product, WorkerRole.ProductManager, team, Q))
                advices.AppendLine("* Hire product manager to boost rating gain and max feature level");

            if (Teams.IsNeedsToHireRole(product, WorkerRole.MarketingLead, team, Q))
                advices.AppendLine("* Hire marketing lead to boost your marketing efficiency");

            if (Teams.IsNeedsToHireRole(product, WorkerRole.TeamLead, team, Q))
                advices.AppendLine("* Hire team lead to boost iteration speed");
        }

        bool hasManagersForThisTeam = Teams.GetCandidatesForTeam(Flagship, team, Q).Count > 0;
        if (!hasManagersForThisTeam)
        {
            advices.Clear();

            advices.AppendLine("<b>You will get new employees next month</b>\nUpdate page if nothing changed");
        }

        //Advices.GetComponent<Blinker>().enabled = !hasLeadManager;
        Advices.text = advices.ToString();
    }
}
﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreenView : View
{
    public GameObject ManagerFocus;
    public GameObject PromoteTeam;

    public Text Advices;

    public GameObject FireTeam;


    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;
        var team = product.team.Teams[SelectedTeam];


        var mainManagerRole = Teams.GetMainManagerForTheTeam(team);
        var formattedManager = Humans.GetFormattedRole(mainManagerRole);

        bool hasLeadManager = Teams.HasMainManagerInTeam(team, Q, product);

        Draw(ManagerFocus, false);
        Draw(PromoteTeam, Teams.IsTeamPromotable(product, team, Q));

        Draw(FireTeam, SelectedTeam > 0);

        // --------------------------------------
        //RenderTeamStats(product);

        // hiring advice ---------------------------------
        RenderTeamAdvices(product, hasLeadManager, formattedManager, team);
    }

    void RenderTeamStats(GameEntity product)
    {
        var ratingGain = Products.GetFeatureRatingGain(product);
        var marketingEffeciency = Teams.GetMarketingEfficiency(product);
        var featureCap = Products.GetFeatureRatingCap(product);
        var devSpeed = Products.GetBaseIterationTime(Q, product);

        var stats = new StringBuilder()
            .Append("Max feature level: ")
            .AppendLine(Visuals.Positive(featureCap.ToString("0.0lvl (")) + Visuals.Positive(Format.ShowChange(ratingGain) + "lvl)"))

            .Append("Marketing efficiency: ")
            .AppendLine(Visuals.Positive(marketingEffeciency.ToString("0") + "%"))
            ;
    }

    void RenderTeamAdvices(GameEntity product, bool hasLeadManager, string formattedManager, TeamInfo team)
    {
        var advices = new StringBuilder();

        if (!hasLeadManager)
        {
            advices.AppendLine(Visuals.Negative($"Hire {formattedManager} to manage this team properly!"));
        }
        else
        {
            if (Teams.IsNeedsToHireRole(product, WorkerRole.ProductManager, team, Q))
                advices.AppendLine("* Product manager boosts MAX feature level");

            if (Teams.IsNeedsToHireRole(product, WorkerRole.MarketingLead, team, Q))
                advices.AppendLine("* Marketing lead boosts your marketing efficiency");

            if (Teams.IsNeedsToHireRole(product, WorkerRole.TeamLead, team, Q))
                advices.AppendLine("* Team lead boosts development speed");
        }

        bool hasManagersForThisTeam = Teams.GetCandidatesForTeam(product, team, Q).Count > 0;
        if (!hasManagersForThisTeam)
        {
            advices.Clear();

            advices.AppendLine("<b>You will get new employees next month</b>\nUpdate page if nothing changed");
        }

        //Advices.GetComponent<Blinker>().enabled = !hasLeadManager;
        Advices.text = advices.ToString();
    }
}

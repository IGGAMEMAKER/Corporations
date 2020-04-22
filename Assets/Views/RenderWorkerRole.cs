using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderWorkerRole : View
{
    public Text Description;
    public override void ViewRender()
    {
        base.ViewRender();

        var Link = GetComponent<LinkToProjectView>();

        var text = "";
        var human = SelectedHuman;

        var role = Humans.GetRole(human);

        var formattedRole = Humans.GetFormattedRole(role);
        var rating = Humans.GetRating(Q, human);

        text = $"{formattedRole} ({rating}LVL)";

        bool isUnemployed = !human.hasWorker || human.worker.companyId == -1;

        if (isUnemployed)
        {
            text += " (Unemployed)";
            Link.enabled = false;

            Description.text = GetRoleDescription(role, isUnemployed);
        }
        else
        {
            var companyId = human.worker.companyId;

            var c = Companies.Get(Q, companyId);

            text += " " + Visuals.Link($"in {c.company.Name}");

            Link.enabled = true;
            Link.CompanyId = companyId;

            Description.text = GetRoleDescription(role, isUnemployed, c);
        }


        GetComponent<Text>().text = text;
    }

    string RenderBonus(long b) => Visuals.Positive(b.ToString());
    string RenderBonus(float b) => Visuals.Positive(b.ToString());

    string GetRoleDescription(WorkerRole role, bool isUnemployed, GameEntity company = null)
    {
        var description = "";
        bool employed = !isUnemployed;

        switch (role)
        {
            case WorkerRole.CEO:            description = $"Increases innovation chances";
                if (employed)
                    description += $" by {RenderBonus(Teams.GetCEOInnovationBonus(company, Q))}%";
                break;

            case WorkerRole.TeamLead:       description = $"Increases team speed";
                if (employed)
                    description += $" by {RenderBonus(Teams.GetTeamLeadDevelopmentTimeDiscount(Q, company))}%";
                break;

            case WorkerRole.MarketingLead:  description = $"Gives more clients";
                if (employed)
                    description += $" by {RenderBonus(Teams.GetMarketingLeadBonus(company, Q))}%";
                break;

            case WorkerRole.ProductManager: description = $"Increases innovation chances";
                if (employed)
                    description+= $" by {RenderBonus(Teams.GetProductManagerBonus(company, Q))}%";
                break;
            case WorkerRole.ProjectManager: description = $"Reduces amount of workers";
                if (employed)
                    description += $" by {RenderBonus(Teams.GetProjectManagerWorkersDiscount(company, Q))}%";
                break;

            case WorkerRole.MarketingDirector:
            case WorkerRole.TechDirector:
            case WorkerRole.Universal:
            default:
                description = "";
                break;
        }

        return description;
    }
}

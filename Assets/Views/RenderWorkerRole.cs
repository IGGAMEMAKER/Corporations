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
        }
        else
        {
            var companyId = human.worker.companyId;

            var c = Companies.Get(Q, companyId);

            text += " " + Visuals.Link($"in {c.company.Name}");

            Link.enabled = true;
            Link.CompanyId = companyId;
        }

        Description.text = GetRoleDescription(role, isUnemployed);

        GetComponent<Text>().text = text;
    }

    string GetRoleDescription(WorkerRole role, bool isUnemployed)
    {
        var description = "";

        switch (role)
        {
            case WorkerRole.CEO:            description = $"Increases innovation chances"; break;
            case WorkerRole.TeamLead:       description = $"Increases team speed"; break;
            case WorkerRole.MarketingLead:  description = $"Gives more clients and brand power"; break;

            case WorkerRole.ProductManager: description = $"Increases innovation chances"; break;
            case WorkerRole.ProjectManager: description = $"Reduces necessary amount of workers"; break;

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

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

        if (!human.hasWorker || human.worker.companyId == -1)
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

        Description.text = GetRoleDescription(role);

        GetComponent<Text>().text = text;
    }

    string GetRoleDescription(WorkerRole role)
    {
        var description = "";

        switch (role)
        {
            case WorkerRole.CEO: description = "Increases innovation chances"; break;
            case WorkerRole.MarketingLead: description = "Gives more clients and brand power"; break;
            case WorkerRole.ProductManager: description = "Increases innovation chances"; break;
            case WorkerRole.ProjectManager: description = "Reduces necessary amount of workers"; break;
            case WorkerRole.TeamLead: description = "Increases team speed"; break;

            case WorkerRole.MarketingDirector: description = ""; break;
            case WorkerRole.TechDirector: description = ""; break;

            case WorkerRole.Universal:
            default:
                description = "";
                break;
        }

        return description;
    }
}

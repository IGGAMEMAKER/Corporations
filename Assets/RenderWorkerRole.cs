using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderWorkerRole : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var Link = GetComponent<LinkToProjectView>();

        var text = "";
        var human = SelectedHuman;

        if (!human.hasWorker)
        {
            text = "Unemployed";
            Link.enabled = false;
        }
        else
        {
            var companyId = human.worker.companyId;

            var c = Companies.GetCompany(GameContext, companyId);

            var role = HumanUtils.GetRole(human);

            text = Visuals.Link( $"{HumanUtils.GetFormattedRole(role)} ({HumanUtils.GetOverallRating(GameContext, human)}LVL) in {c.company.Name}" );
            Link.enabled = true;
            Link.CompanyId = companyId;
        }

        GetComponent<Text>().text = text;
    }
}

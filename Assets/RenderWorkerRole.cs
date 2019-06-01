using Assets.Utils;
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

            var c = CompanyUtils.GetCompanyById(GameContext, companyId);

            var role = HumanUtils.GetRole(GameContext, human);

            text = Visuals.Link( $"{HumanUtils.GetFormattedRole(role)} in {c.company.Name}" );
            Link.enabled = true;
            Link.CompanyId = companyId;
        }

        GetComponent<Text>().text = text;
    }
}

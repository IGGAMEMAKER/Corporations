using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireManagerByRole : CompanyUpgradeButton
{
    public WorkerRole WorkerRole;
    public RenderCompanyRoleOrHireWorkerWithThatRole renderCompanyRoleOrHireWorkerWithThatRole;

    public override void Execute()
    {
        var company = Flagship;

        //FindObjectOfType<ManagerTabRelay>().HireWorker(WorkerRole);
    }

    public override string GetBenefits()
    {
        return Teams.GetRoleDescription(WorkerRole, Q, true);
    }

    public override string GetButtonTitle()
    {
        return "Hire " + Humans.GetFormattedRole(WorkerRole);
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}

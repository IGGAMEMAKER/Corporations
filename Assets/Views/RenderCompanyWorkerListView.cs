using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    bool roleWasSelected = false;
    WorkerRole SelectedWorkerRole;

    public GameObject CompanyUpgrades;
    public GameObject MarketingCampaigns;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        bool highlightRole = !roleWasSelected || (roleWasSelected && role == SelectedWorkerRole);
        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, this, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            SetItems(roles);

            RenderCompanyUpgrades();
        }
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }

    void HighlightManagers()
    {
        foreach (Transform child in transform)
        {
            var c = child.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>();

            var role = c.role;

            bool thisExactRoleWasSelected = roleWasSelected && role == SelectedWorkerRole;

            bool highlightRole = !roleWasSelected || thisExactRoleWasSelected;

            c.HighlightWorkerRole(highlightRole);

            // hide upgrades
            if (!thisExactRoleWasSelected)
                c.workerActions.HideActions();

            //Draw(c.workerActions.Upgrades, thisExactRoleWasSelected);
        }
    }

    void RenderCompanyUpgrades()
    {
        Draw(CompanyUpgrades, roleWasSelected);
    }

    public void SetRole(WorkerRole role)
    {
        this.SelectedWorkerRole = role;
        this.roleWasSelected = true;

        CompanyUpgrades.GetComponent<ProductUpgradeButtons>().WorkerRole = role;
        RenderCompanyUpgrades();
        HighlightManagers();
    }

    public void ResetRoles()
    {
        roleWasSelected = false;

        RenderCompanyUpgrades();
        HighlightManagers();
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyWorkerListView : ListView
{
    GameEntity company;

    FlagshipRelayInCompanyView _flagshipRelay;
    FlagshipRelayInCompanyView flagshipRelay
    {
        get
        {
            if (_flagshipRelay == null)
            {
                _flagshipRelay = FindObjectOfType<FlagshipRelayInCompanyView>();
            }

            return _flagshipRelay;
        }
    }

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var role = (WorkerRole)(object)entity;

        bool highlightRole = flagshipRelay.IsRoleChosen(role);

        t.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(company, role, highlightRole);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (company != null)
        {
            var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);

            SetItems(roles);
        }
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        ViewRender();
    }

    public void HighlightManagers()
    {
        foreach (Transform child in transform)
        {
            var c = child.GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>();

            bool IsRoleActive = flagshipRelay.IsRoleChosen(c.role);

            c.HighlightWorkerRole(IsRoleActive);
        }
    }

    private void OnDisable()
    {
        //roleWasSelected = false;
    }
}

using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCommunicationEffeciencyLine : View
{
    public Image Line;

    public void SetEntity(GameEntity company, WorkerRole role, int maxRoles, Transform Center)
    {
        Line.color = Visuals.GetGradientColor(0, 100, Random.Range(0, 100));

        var angle = GetRoleId(role);
        var angleRad = angle * Mathf.Deg2Rad;

        //Line.transform.Rotate(0,0,angle, Space.Self);
        Line.transform.Rotate(0, 0, angle, Space.Self);

        var offset = 190f;
        Line.transform.Translate(Mathf.Sin(angleRad) * offset, Mathf.Cos(angleRad) * offset, 0, Space.World);
    }

    int GetRoleId(WorkerRole workerRole)
    {
        switch (workerRole)
        {
            case WorkerRole.MarketingLead: return -180;
            case WorkerRole.TeamLead: return 270;
            case WorkerRole.ProductManager: return 90;
            case WorkerRole.ProjectManager: return 0;

            default: return 0;
        }
    }
}

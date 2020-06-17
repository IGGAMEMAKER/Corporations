using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFlagshipCEOWorkerRole : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>().SetEntity(Flagship, WorkerRole.CEO, true);
    }
}

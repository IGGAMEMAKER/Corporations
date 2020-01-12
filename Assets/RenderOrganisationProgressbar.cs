using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrganisationProgressbar : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var organisation = SelectedCompany.team.Organisation;

        GetComponent<ProgressBar>().SetValue(organisation, 100);
    }
}

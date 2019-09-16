using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Utils;

public class IterationDuration : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<RenderConceptProgress>().SetCompanyId(SelectedCompany.company.Id);
    }

    //public override string RenderValue()
    //{
    //    return ProductUtils.IsWillInnovate(SelectedCompany, GameContext) ?
    //        "Will innovate in" :
    //        "Will upgrade in";
    //}
}

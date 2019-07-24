using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideProductOverViewTabIfNotNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !SelectedCompany.hasProduct;
    }
}

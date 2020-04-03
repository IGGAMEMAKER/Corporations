using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenAnyCompanyChanges : Controller
    , IAnyCompanyListener
{
    public override void AttachListeners()
    {
        AnyChangeListener().AddAnyCompanyListener(this);
    }

    public override void DetachListeners()
    {
        AnyChangeListener().RemoveAnyCompanyListener(this);
    }

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        Render();
    }
}

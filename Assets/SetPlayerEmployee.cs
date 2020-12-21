using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerEmployee : View
{
    private void OnEnable()
    {
        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();
        
        GetComponentInChildren<HumanPreview>().SetEntity(Hero.human.Id);
        
    }
}

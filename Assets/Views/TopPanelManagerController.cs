using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelManagerController : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<TopPanelManager>().Load();
    }
}

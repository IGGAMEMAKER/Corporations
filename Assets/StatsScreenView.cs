using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;

public class StatsScreenView : MonoBehaviour {
    internal void Redraw(List<ProductComponent> projects, int myCompanyId)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["myCompanyId"] = myCompanyId;
    }
}

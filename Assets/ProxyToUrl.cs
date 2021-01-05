using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyToUrl : View
{
    public string Url;
    
    private void OnEnable()
    {
        OpenUrl(Url);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrl : ButtonController
{
    public string Url;

    public override void Execute()
    {
        OpenUrl(Url);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventView : View
{
    public Text Title;
    public Text Counter;

    public string title;
    public int counter;
    
    private void OnValidate()
    {
        Title.text = title;
        Counter.text = $"({counter})";
    }
}

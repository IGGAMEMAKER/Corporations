using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum TextStyle
{
    None,
    ScreenTitle,
    PanelTitle,
    PanelText,
    
    KpiLabel,
    KpiValue
}

public class Styler : MonoBehaviour
{
    [FormerlySerializedAs("TextStyle")] public TextStyle textStyle;
    
    [ExecuteInEditMode]
    void OnValidate()
    {
        var txt = GetComponent<Text>();
        
        if (txt == null)
            return;

        switch (textStyle)
        {
            case TextStyle.None:
                break;
            case TextStyle.ScreenTitle:
                break;
            
            case TextStyle.PanelText:
                txt.fontSize = 22;
                txt.fontStyle = FontStyle.Bold;
                break;
            case TextStyle.PanelTitle:
                txt.fontSize = 34;
                txt.fontStyle = FontStyle.Normal;
                break;
            
            case TextStyle.KpiLabel:
                txt.fontSize = 34;
                txt.fontStyle = FontStyle.Normal;
                break;
            case TextStyle.KpiValue:
                txt.fontSize = 60;
                txt.fontStyle = FontStyle.Bold;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

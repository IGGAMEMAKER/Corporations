using System;
using Assets.Core;
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
    KpiValue,
    
    PlainDescription,
    
    SmallText,
    Link
}

public enum ImageColor
{
    None,
    Primary,
    Secondary,
}

public class Styler : MonoBehaviour
{
    [FormerlySerializedAs("TextStyle")] public TextStyle textStyle;
    public ImageColor imageColor;

    void OnValidate()
    {
        var txt = GetComponent<Text>();

        if (txt != null)
        {
            SetTextComponent(txt);
        }

        var img = GetComponent<Image>();

        if (img != null)
        {
            SetImageComponentColor(img);
        }
    }

    void SetImageComponentColor(Image img)
    {
        switch (imageColor)
        {
            case ImageColor.None:
                break;
            case ImageColor.Primary:
                img.color = Visuals.GetColorFromString($"#190061"); // new Color(0.34f, 0f, 1f);
                // img.color = Visuals.GetColorFromString($"#0C0032"); // new Color(0.34f, 0f, 1f);
                // 250090
                break;
            case ImageColor.Secondary:
                img.color = Visuals.GetColorFromString($"#4A0061");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void SetTextComponent(Text txt)
    {
        txt.color = Color.white;
        
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
                txt.fontStyle = FontStyle.Bold;
                break;
            case TextStyle.KpiValue:
                txt.fontSize = 60;
                txt.fontStyle = FontStyle.Normal;
                break;

            case TextStyle.PlainDescription:
                txt.fontSize = 28;
                txt.fontStyle = FontStyle.Normal;
                break;
            case TextStyle.SmallText:
                txt.fontSize = 20;
                txt.fontStyle = FontStyle.Normal;
                
                break;
            case TextStyle.Link:
                txt.color = Visuals.Link();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
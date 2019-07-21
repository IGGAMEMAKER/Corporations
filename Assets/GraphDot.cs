using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDot : MonoBehaviour
{
    public Text Text;
    public Image Image;
    public Hint Hint;

    public void Render(Color color, string value, string hint)
    {
        Text.text = value;
        Image.color = color;

        Hint.SetHint($"{hint} {value}");
    }
}

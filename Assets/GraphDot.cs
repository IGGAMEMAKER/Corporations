using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDot : MonoBehaviour
{
    public Text Text;
    public Image Image;

    public void Render(Color color, string value)
    {
        Text.text = value;
        Image.color = color;
    }
}

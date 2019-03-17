using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
//[RequireComponent(typeof(Image))]
public class IsChosenComponent : MonoBehaviour
{
    Image Image;
    Text Text;
    Color BackgroundColor;
    Color TextColor;


    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();

        BackgroundColor = Image.color;
        TextColor = Text.color;

        Image.color = Color.blue;
        Text.color = Color.white;
    }

    private void OnDestroy()
    {
        // Restore default color
        Image.color = BackgroundColor;
        Text.color = TextColor;
    }
}

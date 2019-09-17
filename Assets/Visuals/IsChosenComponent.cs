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

    bool state = false;

    public bool IsChosen
    {
        get
        {
            return state;
        }
    }

    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();

        BackgroundColor = Color.white; // Image.color;
        TextColor = Text.color;

        Toggle(false);
    }

    public void Toggle(bool isChosen)
    {
        if (isChosen)
            PaintIt();
        else
            RestoreDefaultColor();

        state = isChosen;
    }

    public void Toggle()
    {
        state = !state;

        if (state)
            PaintIt();
        else
            RestoreDefaultColor();
    }

    void RestoreDefaultColor()
    {
        //if (Image != null)

        //if (Text != null)
            Image.color = Color.white;
            Text.color = Color.black;
    }

    void PaintIt()
    {
        if (Image == null || Text == null)
            return;

        Image.color = Color.blue;
        Text.color = Color.white;
    }
}

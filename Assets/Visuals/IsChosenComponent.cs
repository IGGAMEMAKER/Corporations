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

    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();

        BackgroundColor = Image.color;
        TextColor = Text.color;

        Toggle(false);
    }

    void RestoreDefaultColor()
    {
        if (Image != null)
            Image.color = BackgroundColor;

        if (Text != null)
            Text.color = TextColor;
    }

    void PaintIt()
    {
        if (Image == null || Text == null)
            return;

        Image.color = Color.blue;
        Text.color = Color.white;
    }
}

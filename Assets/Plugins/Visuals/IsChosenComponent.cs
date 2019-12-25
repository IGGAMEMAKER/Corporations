using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Button))]
//[RequireComponent(typeof(Image))]
public class IsChosenComponent : MonoBehaviour
{
    Image Image;
    Text Text;

    public bool IsChosen { get; private set; } = false;

    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();


        Toggle(false);
    }

    public void Toggle(bool isChosen)
    {
        if (isChosen)
            PaintIt();
        else
            RestoreDefaultColor();

        IsChosen = isChosen;
    }

    public void Toggle()
    {
        IsChosen = !IsChosen;

        if (IsChosen)
            PaintIt();
        else
            RestoreDefaultColor();
    }

    void RestoreDefaultColor()
    {
        if (Image != null)
            Image.color = Color.white;

        if (Text != null)
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

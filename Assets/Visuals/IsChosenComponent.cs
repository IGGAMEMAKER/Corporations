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

    void Start()
    {
        Image = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();

        BackgroundColor = Image.color;
        TextColor = Text.color;

        PaintIt();
    }

    private void OnEnable()
    {
        PaintIt();
    }

    private void OnDisable()
    {
        RestoreDefaultColor();
    }

    private void OnDestroy()
    {
        RestoreDefaultColor();
    }

    void RestoreDefaultColor()
    {
        // Restore default color

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

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

        PaintIt();
    }

    void RestoreDefaultColor()
    {
        // Restore default color

        if (Image != null)
            Image.color = BackgroundColor;

        if (Text != null)
            Text.color = TextColor;
    }

    private void OnEnable()
    {
        PaintIt();
    }

    void PaintIt()
    {
        if (Image == null || Text == null)
            return;

        Image.color = Color.blue;
        Text.color = Color.white;
    }

    private void OnDisable()
    {
        RestoreDefaultColor();
    }

    private void OnDestroy()
    {
        RestoreDefaultColor();
    }
}

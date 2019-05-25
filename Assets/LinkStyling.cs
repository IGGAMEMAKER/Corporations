using Assets.Utils;
using UnityEngine.UI;

public class LinkStyling : View
{
    void Start()
    {
        Render();
    }

    void Render()
    {
        Text Text = GetComponent<Text>();

        Text.text = VisualUtils.Link(Text.text);
    }
}

using Assets.Core;
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

        Text.text = Visuals.Link(Text.text);
    }
}

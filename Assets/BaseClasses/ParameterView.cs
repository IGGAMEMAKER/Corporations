using UnityEngine.UI;

public abstract class ParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void Start()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void Render()
    {
        Text.text = RenderValue();
        Hint.SetHint(RenderHint());
    }

    void Update()
    {
        Render();
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}

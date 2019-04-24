using UnityEngine.UI;

public abstract class SimpleParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void Start()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    public void Render()
    {
        Text.text = RenderValue();
        string hint = RenderHint();

        if (hint.Length > 0)
            Hint.SetHint(hint);
    }

    void Update()
    {
        Render();
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}

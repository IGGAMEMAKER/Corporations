using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hint))]
public class ResourceView : MonoBehaviour {
    Text Text;
    Hint Hint;

    private void Awake()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    void SetValue<T> (T value, string hint)
    {
        Set(ValueFormatter.NoFormatting(value), hint);
    }

    void Set(string text, string hint)
    {
        Text.text = text;
        Hint.SetHint(hint);
    }

    public void UpdateResourceValue<T>(string hint, T value)
    {
        SetValue(value, hint);
    }

    public void SetPrettifiedValue<T>(string hint, T value)
    {
        Set(ValueFormatter.Shorten(value), hint);
    }
}

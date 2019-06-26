using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hint))]
public class ResourceView : MonoBehaviour {
    void SetValue<T> (T value, string hint)
    {
        Set(Format.NoFormatting(value), hint);
    }

    void Set(string text, string hint)
    {
        GetComponent<Text>().text = text;
        GetComponent<Hint>().SetHint(hint);
    }

    public void UpdateResourceValue<T>(string hint, T value)
    {
        SetValue(value, hint);
    }

    public void SetPrettifiedValue<T>(string hint, T value)
    {
        Set(Format.Minify(value), hint);
    }
}

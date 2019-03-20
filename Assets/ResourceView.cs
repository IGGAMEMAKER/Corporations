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
        Text.text = ValueFormatter.NoFormatting(value);
        Hint.SetHint(hint);
    }

    public void UpdateResourceValue<T>(string hint, T value)
    {
        SetValue(value, hint);
    }
}

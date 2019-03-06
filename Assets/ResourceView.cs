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
        Text.text = ShortenValue(value);
        Hint.SetHint(hint);
    }

    public void UpdateResourceValue<T>(string hint, T value)
    {
        SetValue(value, hint);
    }

    string ShortenValue <T> (T value)
    {
        return value.ToString();

        long.TryParse(value.ToString(), out long val);

        long trillion = 1000000000000;
        long billion = 1000000000;
        long million = 1000000;
        long thousand = 1000;

        if (val > trillion)
            return (int)(val / trillion) + "T";

        if (val > billion)
            return (int)(val / billion) + "B";

        if (val > million)
            return (int)(val / million) + "M";

        if (val > thousand * 10)
            return (int)(val / thousand) + "k";

        return val.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour {
    string ShortenValue <T> (T value)
    {
        return value.ToString();

        long val = 0;
        long.TryParse(value.ToString(), out val);

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

        if (val > thousand)
            return (int)(val / thousand) + "k";

        return val.ToString();
    }

    void SetResourceAndTheHint<T>(string name, T value, string hint)
    {
        GameObject text = gameObject.transform.GetChild(0).gameObject;
        text.GetComponent<Text>().text = ShortenValue(value);

        text.GetComponentInChildren<Hint>().SetHintObject(hint);

        GameObject icon = gameObject.transform.GetChild(1).gameObject;
        icon.GetComponentInChildren<Hint>().SetHintObject(name);
    }

    // only set the value
    public void UpdateResourceValue<T>(string name, T value)
    {
        SetResourceAndTheHint(name, value, "");
    }

    // set both the value and value month(period) change in hint
    public void UpdateResourceValue<T>(string name, T value, T valueChange)
    {
        SetResourceAndTheHint(name, value, valueChange.ToString());
    }

    // set both the value and value month(period) change in hint
    public void UpdateResourceValue<T>(string name, T value, string valueChange)
    {
        SetResourceAndTheHint(name, value, valueChange);
    }
}

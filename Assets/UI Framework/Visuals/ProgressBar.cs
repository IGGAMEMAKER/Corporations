using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float progress;
    string description = "";
    float required = 100;


    Slider slider;

    public Text Text;

    void Start () {
        progress = 0;
        description = "";
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public void SetValue(float have, float requirement)
    {
        required = requirement;

        SetValue(have);

        var txt = $"{Format.Minify(have)} / {Format.Minify(requirement)}";
        SetText(txt);
    }

    public void SetValue(long have, long requirement)
    {
        required = requirement;

        SetValue(have);

        var txt = $"{Format.Minify(have)} / {Format.Minify(requirement)}";
        SetText(txt);
    }

    void SetText(string txt)
    {
        if (Text != null)
        {
            Text.text = description.Length > 0 ?
                $"{description} ({txt})"
                : txt;
        }
    }

    public void SetValue (float val)
    {
        // val from 0 to 100f
        progress = val;

        if (!slider)
            slider = GetComponent<Slider>();

        slider.value = progress / required;

        var txt = (int)progress + "%";
        SetText(txt);
    }
}

using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float progress;
    Slider slider;

    float required = 100;

    public Text Text;

    //private void Awake()
    //{
        
    //}

    void Start () {
        progress = 0;
    }

    public void SetValue(float have, float requirement)
    {
        required = requirement;

        SetValue(have);

        Text.text = $"{Format.Shorten(have)} / {Format.Shorten(requirement)}";
    }

    public void SetValue (float val)
    {
        // val from 0 to 100f
        progress = val;

        if (!slider)
            slider = GetComponent<Slider>();

        slider.value = progress / required;

        Text.text = (int)progress + "%";
    }
}

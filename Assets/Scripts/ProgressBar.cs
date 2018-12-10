using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float progress;
    Slider slider;

    public Text Text;

	// Use this for initialization
	void Start () {
        progress = 0;
    }

    public void SetValue (float val)
    {
        // val from 0 to 100f
        progress = val;

        if (!slider)
            slider = GetComponent<Slider>();

        slider.value = progress / 100;

        //if (!Text)
        //    Text = GetComponent<Text>();

        //Text.text = progress + "%";
    }
}

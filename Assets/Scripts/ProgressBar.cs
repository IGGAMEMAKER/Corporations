using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float progress;
    Slider slider;

    Text Text;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("Increment", 2.0f, 0.5f);
        progress = 0;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetValue (float val)
    {
        // val from 0 to 100f
        progress = val;

        if (slider == null)
            slider = GetComponent<Slider>();
        slider.value = progress / 100;

        if (Text == null)
            Text = GetComponentInChildren<Text>();

        if (Text)
            Text.text = progress + "%";
    }
}

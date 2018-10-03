using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float value;
    Slider slider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        slider = GetComponent<Slider>();
        slider.value = value / 100;
	}

    public void Refresh (float val)
    {
        value = val;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    float value;
    Slider slider;
    int v = 0;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("Increment", 2.0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        slider = GetComponent<Slider>();
        slider.value = value / 100;
	}

    void Draw ()
    {
        Debug.Log("I am drawing! " + Time.time);
    }

    public void Refresh (float val)
    {
        value = val;
    }

    public void SetValue (float val)
    {
        // val from 0 to 100f
        Refresh(val);
    }

    void Increment ()
    {
        Debug.Log("Refresh " + v);
        v++;
        Refresh(v);
    }
}

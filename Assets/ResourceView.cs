using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetResourceAndTheHint<T>(string name, T value, string hint)
    {
        GameObject text = gameObject.transform.GetChild(0).gameObject;
        text.GetComponent<Text>().text = value.ToString();

        text.GetComponentInChildren<UIHint>().SetHintObject(hint);

        GameObject icon = gameObject.transform.GetChild(1).gameObject;
        icon.GetComponentInChildren<UIHint>().SetHintObject(name);
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

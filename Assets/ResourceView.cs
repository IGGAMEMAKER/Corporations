﻿using System.Collections;
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

    public void UpdateResourceValue<T>(T value)
    {
        GameObject text = gameObject.transform.GetChild(0).gameObject;
        text.GetComponent<Text>().text = value.ToString();
    }
}

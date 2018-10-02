using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintItRed : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.green;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
            print("ROSES ARE RED!");
        }
    }
}

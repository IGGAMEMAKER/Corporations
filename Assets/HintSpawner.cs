using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSpawner : MonoBehaviour {
    public GameObject HintPrefab;

    public GameObject Spawn(GameObject parent)
    {
        return Instantiate(HintPrefab, parent.transform);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

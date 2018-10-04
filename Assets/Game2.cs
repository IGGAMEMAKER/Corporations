using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int featureCount = 4;

        List<int> features = Enumerable.Repeat(0, featureCount).ToList();

        Debug.Log("Printing elements!");
        for (int i = 0; i < features.Count; i++)
        {
            Debug.Log("Printing element[" + i + "]: " + features[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

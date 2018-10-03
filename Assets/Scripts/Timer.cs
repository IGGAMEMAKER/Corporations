using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    int tick = 0;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Tick", 0, 1);
    }

    public void Tick ()
    {
        tick++;
    }
}

using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public World world;

    // Use this for initialization
    void Start () {
        world = new World();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public World GetWorld()
    {
        return world;
    }
}

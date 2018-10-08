using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {
    World world;

    // Use this for initialization
    void Start () {
        world = new World();

        world.PrintResources(0);
        world.ExploreFeature(0, 0);
        world.UpgradeFeature(0, 0);
        world.PrintResources(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

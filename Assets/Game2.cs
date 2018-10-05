using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game2 : MonoBehaviour {
    public static int featureCount = 4;
    public int baseTechCost = 50;
    public int maxAmountOfTraits = 3;

    List<Project> Projects;
    List<Market> Markets;

    // Use this for initialization
    void Start () {
        int[] mySkills = new int[] { 1, 7, 3 };
        int[] myTraits = new int[] { };
        Human me = new Human("Gaga", "Iosebashvili", mySkills, myTraits, 1, 500);

        List<Human> workers = new List<Human>{ me };
        Project p = new Project(featureCount, workers, new TeamResource(100, 100, 100, 10, 5000));

        Projects = new List<Project>{ p };

        Projects[0].PrintFeatures();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

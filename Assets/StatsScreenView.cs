using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;

public class StatsScreenView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Redraw(List<Project> projects)
    {
        gameObject.GetComponentInChildren<CompanyListRenderer>().UpdateList(projects);
    }
}

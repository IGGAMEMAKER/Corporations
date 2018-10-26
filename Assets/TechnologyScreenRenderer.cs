using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyScreenRenderer : MonoBehaviour
{
    GameObject screen;
    public void RenderFeatures(List<Feature> features)
    {
        Debug.LogFormat("Render features in TechnologyScreenRenderer {0}", features.Count);

        screen = gameObject.transform.Find("FeatureRenderer").gameObject;

        screen.GetComponent<FeatureListRenderer>().UpdateList(features);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

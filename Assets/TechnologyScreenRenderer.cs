using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyScreenRenderer : MonoBehaviour
{
    GameObject screen;

    public void RenderFeatures(List<Feature> features)
    {
        screen = gameObject.transform.Find("FeatureRenderer").gameObject;

        screen.GetComponent<FeatureListRenderer>().UpdateList(features);
    }
}

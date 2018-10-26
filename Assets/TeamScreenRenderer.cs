using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScreenRenderer : MonoBehaviour
{
    GameObject screen;
    public void RenderFeatures(List<Feature> features)
    {
        screen = gameObject.transform.Find("FeatureRenderer").gameObject;

        screen.GetComponent<WorkerListRenderer>().UpdateList(features);
    }
}

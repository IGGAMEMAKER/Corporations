using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologyScreenRenderer : MonoBehaviour
{
    public GameObject FeaturePrefab;
    public ListContentManager FeatureList;

    public void Render(List<Feature> features)
    {
        List<GameObject> items = new List<GameObject>();

        for (int i = 0; i < features.Count; i++)
        {
            Debug.Log("rendering features : " + i);
            GameObject v = Instantiate(FeaturePrefab, transform);

            v.GetComponent<FeatureView>().Render(features[i], i);

            items.Add(v);
        }

        FeatureList.SetContent(items);
    }
}

using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureListRenderer : ListRenderer
{
    public override int itemsPerLine
    {
        get { return 3; }
        set { }
    }

    public override void RenderObject(GameObject gameObject, object item, int index)
    {
        Debug.LogFormat("Render Feature!");
        Feature feature = (Feature)item;

        gameObject.transform.Find("Title").gameObject.GetComponent<Text>().text = feature.name;
        gameObject.transform.Find("RelevancyStatus").gameObject.GetComponent<Text>().text = feature.GetLiteralFeatureStatus();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

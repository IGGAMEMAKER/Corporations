using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertRenderer : MonoBehaviour {
    List<GameObject> advertViews;
    public GameObject advertInstance;

	// Use this for initialization
	void Start () {
        advertViews = new List<GameObject>();
	}

    void RemoveCurrentObjects ()
    {
        for (int i = 0; i < advertViews.Count; i++)
        {
            Destroy(advertViews[i]);
        }

        advertViews.Clear();
    }

    public void UpdateAds(List<Advert> adverts)
    {
        // remove all objects
        RemoveCurrentObjects();

        float spacing = 50f;

        int ADS_PER_LINE = 5;

        for (var i = 0; i < adverts.Count; i++)
        {
            int x = i % ADS_PER_LINE;
            int y = i / ADS_PER_LINE;

            Vector3 pos = new Vector3(x, y, 0) * spacing;
            GameObject g = Instantiate(advertInstance, pos, Quaternion.identity);

            g.transform.SetParent(this.gameObject.transform, false);

            advertViews.Add(g);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

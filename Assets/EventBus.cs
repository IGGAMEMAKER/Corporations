using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void SendCommand(string eventName, Dictionary<string, object> parameters)
    {
        Debug.Log("EventBus -> AdvertController");
        AdvertController advertController = GetComponent<AdvertController>();
        advertController.HandleCommand(eventName, parameters);


        Debug.Log("EventBus -> FeatureController");
        FeatureController featureController = GetComponent<FeatureController>();
        Debug.Log("EventBus -> FeatureController Got Component");
        featureController.HandleCommand(eventName, parameters);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

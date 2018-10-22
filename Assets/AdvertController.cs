using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertController : MonoBehaviour, CommandHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        Debug.LogFormat("Handle command! AdvertController {0} {1} Write switch case here!", eventName, parameters.ToString());
    }
}

public interface CommandHandler
{
    void HandleCommand(string eventName, Dictionary<string, object> parameters);
}

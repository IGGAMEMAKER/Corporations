using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareController : MonoBehaviour, ICommandHandler
{
    Model model;

    public void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {
        switch (eventName)
        {
            case Commands.SHARES_BUY:
                break;

            case Commands.SHARES_SELL:
                break;
        }
    }

    // Use this for initialization
    void Start () {
		model = gameObject.GetComponent<Model>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    Application GetApplication()
    {
        return model.GetWorld();
    }
}

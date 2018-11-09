﻿using System.Collections.Generic;
using UnityEngine;

public interface ICommandHandler
{
    void HandleCommand(string eventName, Dictionary<string, object> parameters);
}

public class BaseCommandHandler : MonoBehaviour, ICommandHandler
{
    Model model;

    public virtual void HandleCommand(string eventName, Dictionary<string, object> parameters)
    {

    }

    public Application application
    {
        get
        {
            return GetApplication();
        }
    }

    public Application GetApplication()
    {
        return model.GetApplication();
    }

    // Use this for initialization
    void Start()
    {
        model = gameObject.GetComponent<Model>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

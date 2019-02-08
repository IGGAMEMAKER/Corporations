using System.Collections.Generic;
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

    public Application2 application
    {
        get
        {
            return GetApplication();
        }
    }

    public Application2 GetApplication()
    {
        return model.GetApplication();
    }

    // Use this for initialization
    void Start()
    {
        model = gameObject.GetComponent<Model>();
    }
}

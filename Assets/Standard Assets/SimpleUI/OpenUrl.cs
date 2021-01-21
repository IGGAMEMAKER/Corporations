using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrl : MonoBehaviour
{
    public string Url;

    Button Button;

    void OnEnable()
    {
        Button = GetComponentInChildren<Button>() ?? GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    void OnDisable()
    {
        RemoveListener();
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(Execute);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void Execute()
    {
        var newUrl = "";
        if (Url.StartsWith("/"))
            newUrl = Url;
        else
            newUrl = "/" + Url;
        
        SimpleUI.OpenUrl(newUrl);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 20);
    }
}
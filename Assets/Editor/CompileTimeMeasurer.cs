using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;

public class CompileTimeMeasurer
{
    [DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Debug.Log($"CompileTimeMeasurer: Compilation finished");


        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}

using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class CompileTimeMeasurer
{
    [DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Debug.Log("CompileTimeMeasurer: Compilation finished");
    }

    void OnUpdate()
    {
        Debug.Log("OnUpdate");
        UnityEditor.Compilation.CompilationPipeline.compilationStarted += CompilationPipeline_compilationStarted;
    }

    private void CompilationPipeline_compilationStarted(object obj)
    {
        var path = (string)obj;

        Debug.Log("Compiled: " + path);
    }
}

using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;

[ExecuteInEditMode]
public class CompileTimeMeasurer : MonoBehaviour
{
    DateTime startTime;

    [DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Debug.Log($"CompileTimeMeasurer: Compilation finished");

        //EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    private void PrintStartCompile(object obj)
    {
        Debug.Log("PrintStartCompile");

        startTime = DateTime.Now;
    }

    private void PrintEndCompile(object obj)
    {
        Debug.Log("PrintEndCompile");
    }

    void OnEnable()
    {
        CompilationPipeline.compilationStarted += PrintStartCompile;
        CompilationPipeline.assemblyCompilationStarted += PrintAssemblyCompiled;
        CompilationPipeline.assemblyCompilationFinished += PrintAssemblyCompileFinished;
        CompilationPipeline.compilationFinished += PrintEndCompile;
    }

    private void PrintAssemblyCompiled(string obj)
    {
        Debug.Log("Compiling " + obj);
    }


    private void PrintAssemblyCompileFinished(string arg1, CompilerMessage[] arg2)
    {
        Debug.Log("Compiled " + arg1);
    }

    void OnDisable()
    {
        CompilationPipeline.compilationStarted -= PrintStartCompile;
        CompilationPipeline.compilationFinished -= PrintEndCompile;
    }
}

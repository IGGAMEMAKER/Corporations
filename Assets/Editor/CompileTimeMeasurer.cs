using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;

[ExecuteInEditMode]
public class CompileTimeMeasurer : MonoBehaviour
{
    [DidReloadScripts]
    static void OnScriptsReloaded()
    {
        Debug.Log($"CompileTimeMeasurer: Update finished");

        //EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    void OnEnable()
    {
        CompilationPipeline.compilationStarted += PrintStartCompile;
        CompilationPipeline.assemblyCompilationStarted += PrintAssemblyCompiled;
        CompilationPipeline.assemblyCompilationFinished += PrintAssemblyCompileFinished;
        CompilationPipeline.compilationFinished += PrintEndCompile;
    }

    void OnDisable()
    {
        CompilationPipeline.compilationStarted -= PrintStartCompile;
        CompilationPipeline.assemblyCompilationStarted -= PrintAssemblyCompiled;
        CompilationPipeline.assemblyCompilationFinished -= PrintAssemblyCompileFinished;
        CompilationPipeline.compilationFinished -= PrintEndCompile;
    }

    private void PrintStartCompile(object obj)
    {
        ClearLogConsole();

        Debug.Log("PrintStartCompile");
    }

    private void PrintEndCompile(object obj)
    {
        Debug.Log($"CompileTimeMeasurer: Compile finished");
    }


    private void PrintAssemblyCompiled(string obj)
    {
        Debug.Log("Compiling " + obj);
    }


    private void PrintAssemblyCompileFinished(string arg1, CompilerMessage[] arg2)
    {
        Debug.Log("Compiled " + arg1);
    }


    // ---------- cleaaning console ------------
    static MethodInfo _clearConsoleMethod;
    static MethodInfo clearConsoleMethod
    {
        get
        {
            if (_clearConsoleMethod == null)
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(SceneView));
                Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                _clearConsoleMethod = logEntries.GetMethod("Clear");
            }
            return _clearConsoleMethod;
        }
    }

    public static void ClearLogConsole()
    {
        clearConsoleMethod.Invoke(new object(), null);
    }
}

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;


//https://gist.github.com/filod/ba1e1522c1821cd24ca1a0c9090eb440#file-asmdefdebug-cs-L59

/// <summary>
/// https://gist.github.com/karljj1/9c6cce803096b5cd4511cf0819ff517b
/// </summary>
[InitializeOnLoad]
public class AsmdefDebug
{
    const string AssemblyReloadEventsEditorPref = "AssemblyReloadEventsTime";
    const string AssemblyCompilationEventsEditorPref = "AssemblyCompilationEvents";
    const string AssemblyCompilationEventsEditorPref2 = "AssemblyCompilationEvents2";
    static readonly int ScriptAssembliesPathLen = "Library/ScriptAssemblies/".Length;
    private static string AssemblyTotalCompilationTimeEditorPref = "AssemblyTotalCompilationTime";

    static Dictionary<string, DateTime> s_StartTimes = new Dictionary<string, DateTime>();

    static StringBuilder s_BuildEvents = new StringBuilder();
    static StringBuilder s_BuildEvents2 = new StringBuilder();
    static double s_CompilationTotalTime;

    static AsmdefDebug()
    {
        CompilationPipeline.assemblyCompilationStarted += CompilationPipelineOnAssemblyCompilationStarted;
        CompilationPipeline.assemblyCompilationFinished += CompilationPipelineOnAssemblyCompilationFinished;
        AssemblyReloadEvents.beforeAssemblyReload += AssemblyReloadEventsOnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += AssemblyReloadEventsOnAfterAssemblyReload;
    }

    static void CompilationPipelineOnAssemblyCompilationStarted(string assembly)
    {
        //Debug.Log($"Starting recompile CompilationPipelineOnAssemblyCompilationStarted");
        
        s_StartTimes[assembly] = DateTime.UtcNow;
    }

    static void CompilationPipelineOnAssemblyCompilationFinished(string assembly, CompilerMessage[] arg2)
    {
        var timeSpan = DateTime.UtcNow - s_StartTimes[assembly];
        s_CompilationTotalTime += timeSpan.TotalMilliseconds;
        s_BuildEvents.AppendFormat("* {0:0.00}s {1}\n", timeSpan.TotalMilliseconds / 1000f,
            assembly.Substring(ScriptAssembliesPathLen, assembly.Length - ScriptAssembliesPathLen));
    }

    static void AssemblyReloadEventsOnBeforeAssemblyReload()
    {
        //Debug.Log($"Starting recompile AssemblyReloadEventsOnBeforeAssemblyReload");

        var totalCompilationTimeSeconds = s_CompilationTotalTime / 1000f;
        s_BuildEvents2.AppendFormat("Compilation total: {0:0.00}s\n", totalCompilationTimeSeconds);

        EditorPrefs.SetString(AssemblyReloadEventsEditorPref, DateTime.UtcNow.ToBinary().ToString());
        EditorPrefs.SetString(AssemblyCompilationEventsEditorPref, s_BuildEvents.ToString());
        EditorPrefs.SetString(AssemblyCompilationEventsEditorPref2, s_BuildEvents2.ToString());
        EditorPrefs.SetString(AssemblyTotalCompilationTimeEditorPref, totalCompilationTimeSeconds.ToString(CultureInfo.InvariantCulture));
    }

    static void AssemblyReloadEventsOnAfterAssemblyReload()
    {
        var binString = EditorPrefs.GetString(AssemblyReloadEventsEditorPref);

        var str = EditorPrefs.GetString(AssemblyTotalCompilationTimeEditorPref);

        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.CurrencyDecimalSeparator = ".";

        // https://stackoverflow.com/questions/1014535/float-parse-doesnt-work-the-way-i-wanted
        var totalCompilationTimeSeconds = float.Parse(str, NumberStyles.Any, ci);


        long bin;
        if (long.TryParse(binString, out bin))
        {
            var date = DateTime.FromBinary(bin);
            var time = DateTime.UtcNow - date;

            var compilationTimes = EditorPrefs.GetString(AssemblyCompilationEventsEditorPref);
            var compilationTimes2 = EditorPrefs.GetString(AssemblyCompilationEventsEditorPref2);
            var totalTimeSeconds = totalCompilationTimeSeconds + time.TotalSeconds;


            if (!string.IsNullOrEmpty(compilationTimes))
            {
                Debug.Log($"Compilation Report: {totalTimeSeconds:F2} seconds\n" + compilationTimes2 + compilationTimes + "Assembly Reload Time: " + time.TotalSeconds + "s\n");
            }
        }
    }
}
#endif
using System;
using UnityEditor;
using UnityEngine;

class CompileTimeMeasurer : AssetPostprocessor
{
    static DateTime start;
    void OnPreprocessAsset()
    {
        start = DateTime.Now;
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var end = DateTime.Now;

        var diff = end.Subtract(start);


        Debug.Log("Timespan: " + start + " " + end);
        Debug.Log("Recompile took: " + diff.TotalSeconds + " seconds");
    }
}
